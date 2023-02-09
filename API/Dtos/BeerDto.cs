namespace API.Dtos
{
    public class BeerDto
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public string BreweryName { get; set; }
		public double AlcoholContent { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
    }
}