using Application.Core;
using Application.Dto;
using Application.Repositories;

using AutoFixture;

using AutoMapper;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Models;

using Moq;

using Persistence;

namespace BreweryManagement.Test.Repository
{
	public class BreweryRepositoryTests
	{
		private async Task<DataContext> GetDataContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "BreweryDatabaseTest")
				.Options;
			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();
			if(await databaseContext.Breweries.CountAsync() <= 0)
			{
				for(int i = 0; i < 3; i++)
				{
					databaseContext.Breweries.Add(new Brewery
					{
						Name = $"Brewery {i}",
						Beers = new List<Beer>
						{
							new Beer
							{
								Name = $"Beer {i}",
								Price = 10 + i,
								AlcoholContent = 1.0 + i,
								Quantity = 10
							},
							new Beer
							{
								Name = $"Beer {i + 1}",
								Price = 11 + i,
								AlcoholContent = 2.0 + i,
								Quantity = 20
							}
						}
					});	
				}
				databaseContext.Breweries.Add(new Brewery { Name = "Brewery 4" });
			}
			await databaseContext.SaveChangesAsync();
			return databaseContext;
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Should_Return_List_Of_Beers()
		{
			//Arrange
			var breweryId = 1;
			var databaseContext = await GetDataContext();
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var repository = new BrewerRepository(databaseContext, mapper);
			
			//Act
			var result = await repository.GetAllBeersByBrewery(breweryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Result<List<BeerDto>>>();
			result.Value.Should().NotBeNull();
			result.IsSuccess.Should().BeTrue();
			result.Error.Should().BeNull();
			result.Message.Should().Be("Beers found successfully");
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Should_Return_Brewery_Does_not_exist()
		{
			//Arrange
			var breweryId = 99;
			var databaseContext = await GetDataContext();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var repository = new BrewerRepository(databaseContext, mapper);

			//Act
			var result = await repository.GetAllBeersByBrewery(breweryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Result<List<BeerDto>>>();
			result.Value.Should().BeNull();
			result.IsSuccess.Should().BeFalse();
			result.Message.Should().BeNull();
			result.Error.Should().Be("Brewery does not exist");
		}

		
		[Fact]
		public async Task GetAllBeersByBrewery_Should_Return_Beer_Does_not_exist()
		{
			//Arrange
			var breweryId = 4;
			var databaseContext = await GetDataContext();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile());
			});
			var mapper = config.CreateMapper();
			var repository = new BrewerRepository(databaseContext, mapper);

			//Act
			var result = await repository.GetAllBeersByBrewery(breweryId);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Result<List<BeerDto>>>();
			result.Value.Should().BeNull();
			result.IsSuccess.Should().BeFalse();
			result.Message.Should().BeNull();
			result.Error.Should().Be("No beers found for this brewery");
		}
	}
}
