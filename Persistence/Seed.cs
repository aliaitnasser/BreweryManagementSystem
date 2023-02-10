using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Persistence
{
	public static class Seed
	{
		public static async Task SeedData(DataContext context)
		{
			if (context.Breweries.Any()) return;
			
			var breweries = new List<Brewery>
			{
				new Brewery { Name = "Brooklyn Brewery", Beers = new List<Beer>()},
				new Brewery { Name = "Budweiser", Beers = new List<Beer>()},
				new Brewery { Name = "Coors", Beers = new List<Beer>()}
			};

			var beers = new List<Beer>
			{
				new Beer
				{
					Name = "Brooklyn Lager",
					Brewery = breweries[0],
					BreweryId = 1,
					AlcoholContent = 5.2,
					Quantity = 100,
					Price = 5.99m
				},
				new Beer
				{
					Name = "Brooklyn Brown Ale",
					Brewery = breweries[0],
					BreweryId = 1,
					AlcoholContent = 3.8,
					Quantity = 90,
					Price = 15.09m
				},
				new Beer
				{
					Name = "Brooklyn Summer Ale",
					Brewery = breweries[0],
					BreweryId = 1,
					AlcoholContent = 4.5,
					Quantity = 100,
					Price = 19.99m
				},
				new Beer
				{
					Name = "Budweiser",
					Brewery = breweries[1],
					BreweryId = 2,
					AlcoholContent = 5.0,
					Quantity = 100,
					Price = 12.99m
				},
				new Beer
				{
					Name = "Bud Light",
					Brewery = breweries[1],
					BreweryId = 2,
					AlcoholContent = 4.2,
					Quantity = 100,
					Price = 10.09m
				},
				new Beer
				{
					Name = "Coors Light",
					Brewery = breweries[2],
					BreweryId = 3,
					AlcoholContent = 4.2,
					Quantity = 100,
					Price = 5.10m
				},
				new Beer
				{
					Name = "Coors Banquet",
					Brewery = breweries[2],
					BreweryId = 3,
					AlcoholContent = 4.8,
					Quantity = 100,
					Price = 8.79m
				},
				new Beer
				{
					Name = "Coors Original",
					Brewery = breweries[2],
					BreweryId = 3,
					AlcoholContent = 5.0,
					Quantity = 100,
					Price = 4.99m
				}
			};

			breweries[0].Beers.Add(beers[0]);
			breweries[0].Beers.Add(beers[1]);
			breweries[0].Beers.Add(beers[2]);
			breweries[1].Beers.Add(beers[3]);
			breweries[1].Beers.Add(beers[4]);
			breweries[2].Beers.Add(beers[5]);
			breweries[2].Beers.Add(beers[6]);
			breweries[2].Beers.Add(beers[7]);

			var wholesalers = new List<Wholesaler>
			{
				new Wholesaler { Name = "GeneDrinks Inc", BeerStocks = new List<BeerStock>()},
				new Wholesaler { Name = "Budweiser Distributors", BeerStocks = new List<BeerStock>()},
				new Wholesaler { Name = "Coors Distributors Inc", BeerStocks = new List<BeerStock>()}
			};

			await context.Breweries.AddRangeAsync(breweries);
			await context.Beers.AddRangeAsync(beers);
			await context.Wholesalers.AddRangeAsync(wholesalers);
			await context.SaveChangesAsync();
		}
	}
}
