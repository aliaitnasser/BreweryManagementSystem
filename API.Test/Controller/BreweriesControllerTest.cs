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
		private Mock<IMapper> _mapper;
		private BreweriesController _controller;
		public BreweriesControllerTest()
		{
			_repository= new Mock<IBrewerRepository>();
			_fixture = new Fixture();
			_mapper = new Mock<IMapper>();
		}

		[Fact]
		public async Task GetAllBeersByBrewery_ReturnsOk()
		{
			//Arrenge
			var breweryId = _fixture.Create<int>();
			var beersDto = _fixture.CreateMany<BeerDto>(breweryId).ToList();
			_repository.Setup(x => x.GetAllBeersByBrewery(breweryId)).ReturnsAsync(Result<List<BeerDto>>.Success(beersDto, "Beers found successfully"));

			//Act
			_controller = new BreweriesController(_repository.Object, _mapper.Object);

			//Assert
			var result = await _controller.GetAllBeersByBrewery(breweryId);
			var obj = result as OkObjectResult;

			Assert.Equal(StatusCodes.Status200OK, obj.StatusCode);
			

		}
	}
}
