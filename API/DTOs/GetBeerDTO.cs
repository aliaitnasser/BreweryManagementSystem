namespace API.DTOs
{
    public class GetBeerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int BreweryId { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
    }
}