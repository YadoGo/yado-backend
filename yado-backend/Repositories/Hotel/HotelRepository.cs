using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models;

namespace yado_backend.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _dbContext;

        public HotelRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Hotel>> GetAllTopHotelsReview()
        {
            return await _dbContext.Hotels
                .OrderByDescending(hotel => hotel.Reviews.Average(review => review.Qualification))
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelById(Guid id)
        {
            return await _dbContext.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsByUserId(Guid userId)
        {
            return await _dbContext.Hotels
                .Where(hotel => hotel.Owners.Any(owner => owner.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsByPopulationId(int populationId, int page, int pageSize)
        {
            return await _dbContext.Hotels
                .Where(hotel => hotel.PopulationId == populationId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetHotelsByParameters(int populationId, Parameter parameters)
        {
            var hotelsByPopulation = await GetAllHotelsByPopulationId(populationId, 1, int.MaxValue);

            var filteredHotels = hotelsByPopulation
                .Where(hotel =>
                    (!parameters.BicycleRental || hotel.Parameter.BicycleRental) &&
                    (!parameters.Solarium || hotel.Parameter.Solarium) &&
                    (!parameters.GolfCourse || hotel.Parameter.GolfCourse) &&
                    (!parameters.GolfCourse || hotel.Parameter.GolfCourse) &&
                    (!parameters.Massage || hotel.Parameter.Massage) &&
                    (!parameters.FitnessCentre || hotel.Parameter.FitnessCentre) &&
                    (!parameters.FreeCancellation || hotel.Parameter.FreeCancellation) &&
                    (!parameters.SelfCatering || hotel.Parameter.SelfCatering) &&
                    (!parameters.BreakfastIncluded || hotel.Parameter.BreakfastIncluded) &&
                    (!parameters.BreakfastDinnerIncluded || hotel.Parameter.BreakfastDinnerIncluded) &&
                    (!parameters.TwinBeds || hotel.Parameter.TwinBeds) &&
                    (!parameters.DoubleBed || hotel.Parameter.DoubleBed) &&
                    (!parameters.LargeDoubleBed || hotel.Parameter.LargeDoubleBed) &&
                    (!parameters.NonSmokingRooms || hotel.Parameter.NonSmokingRooms) &&
                    (!parameters.Parking || hotel.Parameter.Parking) &&
                    (!parameters.VeryGoodWifi || hotel.Parameter.VeryGoodWifi) &&
                    (!parameters.FamilyRooms || hotel.Parameter.FamilyRooms) &&
                    (!parameters.SwimmingPool || hotel.Parameter.SwimmingPool) &&
                    (!parameters.RoomService || hotel.Parameter.RoomService) &&
                    (!parameters.PetsAllowed || hotel.Parameter.PetsAllowed) &&
                    (!parameters.FacilitesDisabledGuest || hotel.Parameter.FacilitesDisabledGuest) &&
                    (!parameters.SpaWellnessCentre || hotel.Parameter.SpaWellnessCentre) &&
                    (!parameters.AeroportSchuttle || hotel.Parameter.AeroportSchuttle) &&
                    (!parameters.Restaurant || hotel.Parameter.Restaurant) &&
                    (!parameters.ReceptionAlwaysOpen || hotel.Parameter.ReceptionAlwaysOpen) &&
                    (!parameters.Kitchen || hotel.Parameter.Kitchen) &&
                    (!parameters.PrivateBathroom || hotel.Parameter.PrivateBathroom) &&
                    (!parameters.AirConditioning || hotel.Parameter.AirConditioning) &&
                    (!parameters.LaptopFriendlyWorkspace || hotel.Parameter.LaptopFriendlyWorkspace) &&
                    (!parameters.Bath || hotel.Parameter.Bath) &&
                    (!parameters.PrivatePool || hotel.Parameter.PrivatePool) &&
                    (!parameters.Terrace || hotel.Parameter.Terrace) &&
                    (!parameters.Balcony || hotel.Parameter.Balcony) &&
                    (!parameters.BlatScreenTv || hotel.Parameter.BlatScreenTv) &&
                    (!parameters.WashingMachine || hotel.Parameter.WashingMachine) &&
                    (!parameters.Patio || hotel.Parameter.Patio) &&
                    (!parameters.Soundproofing || hotel.Parameter.Soundproofing) &&
                    (!parameters.ViewHotel || hotel.Parameter.ViewHotel) &&
                    (!parameters.SeaView || hotel.Parameter.SeaView) &&
                    (!parameters.WheelchairAccessible || hotel.Parameter.WheelchairAccessible) &&
                    (!parameters.ToiletGrabRails || hotel.Parameter.ToiletGrabRails) &&
                    (!parameters.HigherLevelToilet || hotel.Parameter.HigherLevelToilet) &&
                    (!parameters.LowerBathroomSink || hotel.Parameter.LowerBathroomSink) &&
                    (!parameters.EmergencyCordBathroom || hotel.Parameter.EmergencyCordBathroom) &&
                    (!parameters.VisualAidsBraille || hotel.Parameter.VisualAidsBraille) &&
                    (!parameters.VisualAidsTactileSigns || hotel.Parameter.VisualAidsTactileSigns)
                )
                .ToList();

            return filteredHotels;
        }



        public async Task<bool> InsertHotel(Hotel hotel)
        {
            _dbContext.Hotels.Add(hotel);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateHotelById(Guid id, Hotel updatedHotel)
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

        public async Task<bool> DeleteHotelById(Guid id)
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
