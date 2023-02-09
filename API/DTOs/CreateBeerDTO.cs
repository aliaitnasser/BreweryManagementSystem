namespace API.DTOs
{
    public class CreateBeerDTO
    {
		public string Name { get; set; }
		public int BreweryId { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
        
    }
}