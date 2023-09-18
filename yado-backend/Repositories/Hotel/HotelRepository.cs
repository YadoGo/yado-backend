using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _dbContext;

        public HotelRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Hotel>> GetAllTopHotelsReviewAsync()
        {
            return await _dbContext.Hotels
                .OrderByDescending(hotel => hotel.Reviews.Average(review => review.Qualification))
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(Guid id)
        {
            return await _dbContext.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsByUserIdAsync(Guid userId)
        {
            return await _dbContext.Hotels
                .Where(hotel => hotel.Owners.Any(owner => owner.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<HotelSummaryDto>> GetAllHotelsByPopulationIdAsync(int populationId, int page, int pageSize)
        {
            var hotels = await _dbContext.Hotels
                .Where(hotel => hotel.PopulationId == populationId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(hotel => new HotelSummaryDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Description = hotel.Description,
                    Stars = hotel.Stars,
                    Address = hotel.Address,
                    Latitude = hotel.Latitude,
                    Longitude = hotel.Longitude,
                    NumVisited = hotel.NumVisited,
                    PopulationId = hotel.PopulationId,
                    FirstImage = hotel.Images
                        .Where(image => image != null)
                        .OrderBy(image => image.Position)
                        .Select(image => new ImageDto
                        {
                            Id = image.Id,
                            ImagePath = image.ImagePath,
                            Description = image.Description
                        })
                        .FirstOrDefault(),
                })
                .ToListAsync();

            return hotels;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByParametersAsync(int populationId, Parameter parameters)
        {
            var hotelsByPopulation = await GetAllHotelsByPopulationIdAsync(populationId, 1, int.MaxValue);

            var filteredHotels = hotelsByPopulation
                .Where(hotel =>
                    (!parameters.BicycleRental || hotel.Parameters.BicycleRental) &&
                    (!parameters.Solarium || hotel.Parameters.Solarium) &&
                    (!parameters.GolfCourse || hotel.Parameters.GolfCourse) &&
                    (!parameters.GolfCourse || hotel.Parameters.GolfCourse) &&
                    (!parameters.Massage || hotel.Parameters.Massage) &&
                    (!parameters.FitnessCentre || hotel.Parameters.FitnessCentre) &&
                    (!parameters.FreeCancellation || hotel.Parameters.FreeCancellation) &&
                    (!parameters.SelfCatering || hotel.Parameters.SelfCatering) &&
                    (!parameters.BreakfastIncluded || hotel.Parameters.BreakfastIncluded) &&
                    (!parameters.BreakfastDinnerIncluded || hotel.Parameters.BreakfastDinnerIncluded) &&
                    (!parameters.TwinBeds || hotel.Parameters.TwinBeds) &&
                    (!parameters.DoubleBed || hotel.Parameters.DoubleBed) &&
                    (!parameters.LargeDoubleBed || hotel.Parameters.LargeDoubleBed) &&
                    (!parameters.NonSmokingRooms || hotel.Parameters.NonSmokingRooms) &&
                    (!parameters.Parking || hotel.Parameters.Parking) &&
                    (!parameters.VeryGoodWifi || hotel.Parameters.VeryGoodWifi) &&
                    (!parameters.FamilyRooms || hotel.Parameters.FamilyRooms) &&
                    (!parameters.SwimmingPool || hotel.Parameters.SwimmingPool) &&
                    (!parameters.RoomService || hotel.Parameters.RoomService) &&
                    (!parameters.PetsAllowed || hotel.Parameters.PetsAllowed) &&
                    (!parameters.FacilitesDisabledGuest || hotel.Parameters.FacilitesDisabledGuest) &&
                    (!parameters.SpaWellnessCentre || hotel.Parameters.SpaWellnessCentre) &&
                    (!parameters.AeroportSchuttle || hotel.Parameters.AeroportSchuttle) &&
                    (!parameters.Restaurant || hotel.Parameters.Restaurant) &&
                    (!parameters.ReceptionAlwaysOpen || hotel.Parameters.ReceptionAlwaysOpen) &&
                    (!parameters.Kitchen || hotel.Parameters.Kitchen) &&
                    (!parameters.PrivateBathroom || hotel.Parameters.PrivateBathroom) &&
                    (!parameters.AirConditioning || hotel.Parameters.AirConditioning) &&
                    (!parameters.LaptopFriendlyWorkspace || hotel.Parameters.LaptopFriendlyWorkspace) &&
                    (!parameters.Bath || hotel.Parameters.Bath) &&
                    (!parameters.PrivatePool || hotel.Parameters.PrivatePool) &&
                    (!parameters.Terrace || hotel.Parameters.Terrace) &&
                    (!parameters.Balcony || hotel.Parameters.Balcony) &&
                    (!parameters.BlatScreenTv || hotel.Parameters.BlatScreenTv) &&
                    (!parameters.WashingMachine || hotel.Parameters.WashingMachine) &&
                    (!parameters.Patio || hotel.Parameters.Patio) &&
                    (!parameters.Soundproofing || hotel.Parameters.Soundproofing) &&
                    (!parameters.ViewHotel || hotel.Parameters.ViewHotel) &&
                    (!parameters.SeaView || hotel.Parameters.SeaView) &&
                    (!parameters.WheelchairAccessible || hotel.Parameters.WheelchairAccessible) &&
                    (!parameters.ToiletGrabRails || hotel.Parameters.ToiletGrabRails) &&
                    (!parameters.HigherLevelToilet || hotel.Parameters.HigherLevelToilet) &&
                    (!parameters.LowerBathroomSink || hotel.Parameters.LowerBathroomSink) &&
                    (!parameters.EmergencyCordBathroom || hotel.Parameters.EmergencyCordBathroom) &&
                    (!parameters.VisualAidsBraille || hotel.Parameters.VisualAidsBraille) &&
                    (!parameters.VisualAidsTactileSigns || hotel.Parameters.VisualAidsTactileSigns)
                )
                .ToList();

            return filteredHotels.Cast<Hotel>();
        }

        public async Task<bool> InsertHotelAsync(Hotel hotel)
        {
            _dbContext.Hotels.Add(hotel);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateHotelByIdAsync(Guid id, Hotel updatedHotel)
        {
            var existingHotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (existingHotel != null)
            {
                existingHotel.Name = updatedHotel.Name;
                existingHotel.Description = updatedHotel.Description;
                existingHotel.Stars = updatedHotel.Stars;
                existingHotel.Address = updatedHotel.Address;
                existingHotel.Latitude = updatedHotel.Latitude;
                existingHotel.Longitude = updatedHotel.Longitude;

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }

        public async Task<bool> DeleteHotelByIdAsync(Guid id)
        {
            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (hotel != null)
            {
                _dbContext.Hotels.Remove(hotel);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }

}
