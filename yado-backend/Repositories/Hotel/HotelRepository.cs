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

        public async Task<IEnumerable<HotelSummaryDto>> GetHotelsByParametersAsync(ParameterDto parameters, int populationId, int page, int pageSize)
        {
            var query = _dbContext.Hotels
                .Where(hotel => hotel.PopulationId == populationId)
                .AsQueryable();
    

            if (parameters.BicycleRental)
            {
                query = query.Where(hotel => hotel.Parameter.BicycleRental);
            }

            if (parameters.Solarium)
            {
                query = query.Where(hotel => hotel.Parameter.Solarium);
            }

            if (parameters.GolfCourse)
            {
                query = query.Where(hotel => hotel.Parameter.GolfCourse);
            }

            if (parameters.Massage)
            {
                query = query.Where(hotel => hotel.Parameter.Massage);
            }

            if (parameters.FitnessCentre)
            {
                query = query.Where(hotel => hotel.Parameter.FitnessCentre);
            }

            if (parameters.FreeCancellation)
            {
                query = query.Where(hotel => hotel.Parameter.FreeCancellation);
            }

            if (parameters.SelfCatering)
            {
                query = query.Where(hotel => hotel.Parameter.SelfCatering);
            }

            if (parameters.BreakfastIncluded)
            {
                query = query.Where(hotel => hotel.Parameter.BreakfastIncluded);
            }

            if (parameters.BreakfastDinnerIncluded)
            {
                query = query.Where(hotel => hotel.Parameter.BreakfastDinnerIncluded);
            }

            if (parameters.TwinBeds)
            {
                query = query.Where(hotel => hotel.Parameter.TwinBeds);
            }

            if (parameters.DoubleBed)
            {
                query = query.Where(hotel => hotel.Parameter.DoubleBed);
            }

            if (parameters.LargeDoubleBed)
            {
                query = query.Where(hotel => hotel.Parameter.LargeDoubleBed);
            }

            if (parameters.ExtraLargeDoubleBed)
            {
                query = query.Where(hotel => hotel.Parameter.ExtraLargeDoubleBed);
            }

            if (parameters.NonSmokingRooms)
            {
                query = query.Where(hotel => hotel.Parameter.NonSmokingRooms);
            }

            if (parameters.Parking)
            {
                query = query.Where(hotel => hotel.Parameter.Parking);
            }

            if (parameters.VeryGoodWifi)
            {
                query = query.Where(hotel => hotel.Parameter.VeryGoodWifi);
            }

            if (parameters.FamilyRooms)
            {
                query = query.Where(hotel => hotel.Parameter.FamilyRooms);
            }

            if (parameters.SwimmingPool)
            {
                query = query.Where(hotel => hotel.Parameter.SwimmingPool);
            }

            if (parameters.RoomService)
            {
                query = query.Where(hotel => hotel.Parameter.RoomService);
            }

            if (parameters.PetsAllowed)
            {
                query = query.Where(hotel => hotel.Parameter.PetsAllowed);
            }

            if (parameters.FacilitesDisabledGuest)
            {
                query = query.Where(hotel => hotel.Parameter.FacilitesDisabledGuest);
            }

            if (parameters.SpaWellnessCentre)
            {
                query = query.Where(hotel => hotel.Parameter.SpaWellnessCentre);
            }

            if (parameters.AeroportSchuttle)
            {
                query = query.Where(hotel => hotel.Parameter.AeroportSchuttle);
            }

            if (parameters.Restaurant)
            {
                query = query.Where(hotel => hotel.Parameter.Restaurant);
            }

            if (parameters.ReceptionAlwaysOpen)
            {
                query = query.Where(hotel => hotel.Parameter.ReceptionAlwaysOpen);
            }

            if (parameters.Kitchen)
            {
                query = query.Where(hotel => hotel.Parameter.Kitchen);
            }

            if (parameters.PrivateBathroom)
            {
                query = query.Where(hotel => hotel.Parameter.PrivateBathroom);
            }

            if (parameters.AirConditioning)
            {
                query = query.Where(hotel => hotel.Parameter.AirConditioning);
            }

            if (parameters.LaptopFriendlyWorkspace)
            {
                query = query.Where(hotel => hotel.Parameter.LaptopFriendlyWorkspace);
            }

            if (parameters.Bath)
            {
                query = query.Where(hotel => hotel.Parameter.Bath);
            }

            if (parameters.PrivatePool)
            {
                query = query.Where(hotel => hotel.Parameter.PrivatePool);
            }

            if (parameters.Terrace)
            {
                query = query.Where(hotel => hotel.Parameter.Terrace);
            }

            if (parameters.Balcony)
            {
                query = query.Where(hotel => hotel.Parameter.Balcony);
            }

            if (parameters.BlatScreenTv)
            {
                query = query.Where(hotel => hotel.Parameter.BlatScreenTv);
            }

            if (parameters.WashingMachine)
            {
                query = query.Where(hotel => hotel.Parameter.WashingMachine);
            }

            if (parameters.Patio)
            {
                query = query.Where(hotel => hotel.Parameter.Patio);
            }

            if (parameters.Soundproofing)
            {
                query = query.Where(hotel => hotel.Parameter.Soundproofing);
            }

            if (parameters.ViewHotel)
            {
                query = query.Where(hotel => hotel.Parameter.ViewHotel);
            }

            if (parameters.SeaView)
            {
                query = query.Where(hotel => hotel.Parameter.SeaView);
            }

            if (parameters.WheelchairAccessible)
            {
                query = query.Where(hotel => hotel.Parameter.WheelchairAccessible);
            }

            if (parameters.ToiletGrabRails)
            {
                query = query.Where(hotel => hotel.Parameter.ToiletGrabRails);
            }

            if (parameters.HigherLevelToilet)
            {
                query = query.Where(hotel => hotel.Parameter.HigherLevelToilet);
            }

            if (parameters.LowerBathroomSink)
            {
                query = query.Where(hotel => hotel.Parameter.LowerBathroomSink);
            }

            if (parameters.EmergencyCordBathroom)
            {
                query = query.Where(hotel => hotel.Parameter.EmergencyCordBathroom);
            }

            if (parameters.VisualAidsBraille)
            {
                query = query.Where(hotel => hotel.Parameter.VisualAidsBraille);
            }

            if (parameters.VisualAidsTactileSigns)
            {
                query = query.Where(hotel => hotel.Parameter.VisualAidsTactileSigns);
            }

            var hotelsByParameters = await query
                .OrderBy(hotel => hotel.Id)
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
                    Parameters = parameters
                })
                .ToListAsync();

            return hotelsByParameters;
        }
    }
}
