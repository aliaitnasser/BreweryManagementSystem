using Application.Dto;

using AutoMapper;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Beer, BeerDto>();
			CreateMap<CreateBeerDto, Beer>();
			CreateMap<BeerStock, BeerStockDto>();
			CreateMap<BeerStockDto, BeerStock>();
		}
	}
}
