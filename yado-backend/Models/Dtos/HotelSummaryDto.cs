namespace yado_backend.Models.Dtos
{
	public class HotelSummaryDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int NumVisited { get; set; }
        public int PopulationId { get; set; }
        public ImageDto FirstImage { get; set; }
        public ParameterDto Parameters { get; set; }
    }
}

