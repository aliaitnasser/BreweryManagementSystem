using API.Controllers;
using Application.Core;
using Application.Dto;
using Application.Repositories;
using AutoFixture;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controller
{
	public class BreweriesControllerTest
	{
		private Mock<IBrewerRepository> _repository;
		private Fixture _fixture;
		private BreweriesController _controller;
		public BreweriesControllerTest()
		{
			_repository= new Mock<IBrewerRepository>();
			_fixture = new Fixture();
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Returns_Ok()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Success(beersDto, "Beers found successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBrewery(breweryId);
			var obj = result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, obj.StatusCode);
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Returns_BadRequest()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Failure("Beers not found"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBrewery(breweryId);
			var obj = result as BadRequestObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);	
		}

		[Fact]
		public async Task GetAllBeersByBrewery_Returns_NotFound()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Success(null,"Beers not found"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.GetAllBeersByBrewery(breweryId);
			var obj = result as NotFoundObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, obj.StatusCode);
		}

		[Fact]
		public async Task AddBeer_Returns_Ok()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Success(beerDto, "Beer added successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeer(beerDto);
			var obj = result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, obj.StatusCode);
		}

		[Fact]
		public async Task AddBeer_Returns_BadRequest()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Failure("Beer not added"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeer(beerDto);
			var obj = result as BadRequestObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);
		}

		[Fact]
		public async Task AddBeer_Returns_NotFound()
		{
			//Arrenge
			var beerDto = _fixture.Create<CreateBeerDto>();
			_repository.Setup(x => x.AddBeer(beerDto)).ReturnsAsync(Result<CreateBeerDto>.Success(null, "Beer not added"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.AddBeer(beerDto);
			var obj = result as NotFoundObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, obj.StatusCode);
		}

		[Fact]
		public async Task DeleteBeer_Returns_OK()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Success(beerDto, "Beer deleted successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeer(beerId, breweryId);
			var obj = result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, obj.StatusCode);	
		}

		[Fact]
		public async Task DeleteBeer_Returns_BadRequest()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Failure("Beer not deleted"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeer(beerId, breweryId);
			var obj = result as BadRequestObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status400BadRequest, obj.StatusCode);
		}

		[Fact]
		public async Task DeleteBeer_Returns_NotFound()
		{
			//Arrenge
			var beerId = _fixture.Create<int>();
			var breweryId = _fixture.Create<int>();
			var beerDto = _fixture.Create<BeerDto>();
			_repository.Setup(x => x.DeleteBeer(beerId, breweryId)).ReturnsAsync(Result<BeerDto>.Success(null, "Beer not deleted"));

			//Act
			_controller = new BreweriesController(_repository.Object);
			var result = await _controller.DeleteBeer(beerId, breweryId);
			var obj = result as NotFoundObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, obj.StatusCode);
		}
	}
}
