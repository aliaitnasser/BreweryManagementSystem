using API.Controllers;

using Application.Core;
using Application.Dto;
using Application.Repositories;

using AutoFixture;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Linq;

namespace BreweryManagement.Test.Controller
{
	public class BreweriesControllerTest
	{
		private Mock<IBrewerRepository> _repository;
		private Fixture _fixture;
		private BreweriesController? _controller;
		public BreweriesControllerTest()
		{
			_repository = new Mock<IBrewerRepository>();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Should_Return_Ok()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Success(beersDto, "Beers found successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBreweryAsync(breweryId);
			var obj = result.Result as OkObjectResult;

			//Assert
			obj.Should().BeOfType<OkObjectResult>();
			obj.Should().NotBeNull();
			obj!.Value.Should().BeOfType<List<BeerDto>>();
		}

		[Fact]
		public async Task GetAllBeersByBrewery__Should_Return_BadRequest()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Failure("Beers not found"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBreweryAsync(breweryId);
			var obj = result.Result as BadRequestObjectResult;

			//Assert
			obj.Should().BeOfType<BadRequestObjectResult>();
			obj.Should().NotBeNull();
			obj!.Value.Should().Be("Beers not found");
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Should_Return_NotFound()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Success(null!, "Beers not found"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBreweryAsync(breweryId);
			var obj = result.Result as NotFoundObjectResult;

			//Assert
			obj.Should().BeOfType<NotFoundObjectResult>();
		}

		[Fact]
		public async Task AddBeer_Should_Return_Ok()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Success(beerDto, "Beer added successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeerAsync(beerDto);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
		}

		[Fact]
		public async Task AddBeer_Should_Return_BadRequest()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Failure("Beer not added"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeerAsync(beerDto);

			//Assert
			result.Should().BeOfType<BadRequestObjectResult>();
		}

		[Fact]
		public async Task AddBeer_Should__Return_NotFound()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Success(null!, "Beer not added"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeerAsync(beerDto);
			var obj = result as NotFoundObjectResult;

			//Assert
			result.Should().BeOfType<NotFoundObjectResult>();
		}

		[Fact]
		public async Task DeleteBeer_Should_Return_OK()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Success(beerDto, "Beer deleted successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeerAsync(beerId, breweryId);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
		}

		[Fact]
		public async Task DeleteBeer_Should_Return_BadRequest()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Failure("Beer not deleted"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeerAsync(beerId, breweryId);

			//Assert
			result.Should().BeOfType<BadRequestObjectResult>();
		}

		[Fact]
		public async Task DeleteBeer_Should_Return_NotFound()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Success(null!, "Beer not deleted"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeerAsync(beerId, breweryId);

			//Assert
			result.Should().BeOfType<NotFoundObjectResult>();
		}
	}
}
