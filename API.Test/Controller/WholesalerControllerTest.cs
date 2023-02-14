﻿using API.Controllers;

using Application.Core;
using Application.Dto;
using Application.Repositories;

using AutoFixture;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Models;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreweryManagement.Test.Controller
{
	public class WholesalerControllerTest
	{
		private Mock<IWholesalerRepository> _repository;
		private Fixture _fixture;
		private WholesalerController _controller;

		public WholesalerControllerTest()
		{
			_repository= new Mock<IWholesalerRepository>();
			_fixture= new Fixture();
		}

		[Fact]
		public async Task AddSale_Should_Return_Ok_And_Message_Sale_added_successfully()
		{
			//Arrenge
			var beerStock = _fixture.Create<BeerStockDto>();
			_repository.Setup(x => x.AddSale(beerStock))
				.ReturnsAsync(Result<BeerStockDto>.Success(beerStock, "Sale added successfully"));

			//Act
			_controller = new WholesalerController(_repository.Object);
			var result = await _controller.AddSale(beerStock);
			var obj = result as OkObjectResult;

			//Assert
			obj.Should().BeOfType<OkObjectResult>();
			obj!.Value.Should().Be("Sale added successfully");
		}

		[Fact]
		public async Task AddSale_Should_Return_BadRequest_Error_Wholesaler_must_exist()
		{
			//Arrenge
			var beerStock = _fixture.Create<BeerStockDto>();
			_repository.Setup(x => x.AddSale(beerStock))
				.ReturnsAsync(Result<BeerStockDto>.Failure("Wholesaler must exist"));

			//Act
			_controller = new WholesalerController(_repository.Object);
			var result = await _controller.AddSale(beerStock);
			var obj = result as BadRequestObjectResult;

			//Assert
			obj.Should().BeOfType<BadRequestObjectResult>();
			obj!.Value.Should().Be("Wholesaler must exist");
		}

		[Fact]
		public async Task AddSale_Should_Return_NotFound()
		{
			//Arrenge
			var beerStock = _fixture.Create<BeerStockDto>();
			_repository.Setup(x => x.AddSale(beerStock))
				.ReturnsAsync(Result<BeerStockDto>.Success(null,"Failed to add sale"));

			//Act
			_controller = new WholesalerController(_repository.Object);
			var result = await _controller.AddSale(beerStock);
			var obj = result as NotFoundObjectResult;

			//Assert
			obj.Should().BeOfType<NotFoundObjectResult>();
			obj!.Value.Should().Be("Failed to add sale");
		}

		[Fact]
		public async Task UpdateRemainingStock_Should_Return_Ok_And_Message_Sale_added_successfully()
		{
			//Arrenge
			var beerStock = _fixture.Create<BeerStockDto>();
			var beerStockId = _fixture.Create<int>();
			var wholesalerId = _fixture.Create<int>();
			var quantity = _fixture.Create<int>();

			_repository.Setup(x => x.UpdateRemainingStock(beerStockId, wholesalerId, quantity))
				.ReturnsAsync(Result<BeerStockDto>.Success(beerStock, "Remaining stock updated successfully"));

			//Act
			_controller = new WholesalerController(_repository.Object);
			var result = await _controller.UpdateRemainingStock(beerStockId, wholesalerId, quantity);
			var obj = result as OkObjectResult;

			//Assert
			obj.Should().BeOfType<OkObjectResult>();
			obj!.Value.Should().Be("Remaining stock updated successfully");
		}
	}
}