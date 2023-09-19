using Microsoft.EntityFrameworkCore;
using yado_backend.Data;
using yado_backend.Models.Dtos;

namespace yado_backend.Repositories
{
	public class ParameterRepository : IParameterRepository
    {
        private readonly AppDbContext _dbContext;

        public ParameterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ParameterDto> GetParametersByHotelIdAsync(Guid hotelId)
        {
            var parameters = await _dbContext.Parameters
                .Where(p => p.HotelId == hotelId)
                .SingleOrDefaultAsync();

            if (parameters == null)
            {
                return null;
            }

            var parameterDto = new ParameterDto
            {
                BicycleRental = parameters.BicycleRental,
                Solarium = parameters.Solarium,
                GolfCourse = parameters.GolfCourse,
                Massage = parameters.Massage,
                FitnessCentre = parameters.FitnessCentre,
                FreeCancellation = parameters.FreeCancellation,
                SelfCatering = parameters.SelfCatering,
                BreakfastIncluded = parameters.BreakfastIncluded,
                BreakfastDinnerIncluded = parameters.BreakfastDinnerIncluded,
                TwinBeds = parameters.TwinBeds,
                DoubleBed = parameters.DoubleBed,
                LargeDoubleBed = parameters.LargeDoubleBed,
                ExtraLargeDoubleBed = parameters.ExtraLargeDoubleBed,
                NonSmokingRooms = parameters.NonSmokingRooms,
                Parking = parameters.Parking,
                VeryGoodWifi = parameters.VeryGoodWifi,
                FamilyRooms = parameters.FamilyRooms,
                SwimmingPool = parameters.SwimmingPool,
                RoomService = parameters.RoomService,
                PetsAllowed = parameters.PetsAllowed,
                FacilitesDisabledGuest = parameters.FacilitesDisabledGuest,
                SpaWellnessCentre = parameters.SpaWellnessCentre,
                AeroportSchuttle = parameters.AeroportSchuttle,
                Restaurant = parameters.Restaurant,
                ReceptionAlwaysOpen = parameters.ReceptionAlwaysOpen,
                Kitchen = parameters.Kitchen,
                PrivateBathroom = parameters.PrivateBathroom,
                AirConditioning = parameters.AirConditioning,
                LaptopFriendlyWorkspace = parameters.LaptopFriendlyWorkspace,
                Bath = parameters.Bath,
                PrivatePool = parameters.PrivatePool,
                Terrace = parameters.Terrace,
                Balcony = parameters.Balcony,
                BlatScreenTv = parameters.BlatScreenTv,
                WashingMachine = parameters.WashingMachine,
                Patio = parameters.Patio,
                Soundproofing = parameters.Soundproofing,
                ViewHotel = parameters.ViewHotel,
                SeaView = parameters.SeaView,
                WheelchairAccessible = parameters.WheelchairAccessible,
                ToiletGrabRails = parameters.ToiletGrabRails,
                HigherLevelToilet = parameters.HigherLevelToilet,
                LowerBathroomSink = parameters.LowerBathroomSink,
                EmergencyCordBathroom = parameters.EmergencyCordBathroom,
                VisualAidsBraille = parameters.VisualAidsBraille,
                VisualAidsTactileSigns = parameters.VisualAidsTactileSigns

            };

            return parameterDto;
        }

    }
}

