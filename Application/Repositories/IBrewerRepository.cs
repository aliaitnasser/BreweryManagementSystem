﻿using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IBrewerRepository
	{
		Task<List<Beer>> GetAllBeers(int breweryId);
		Task AddBeer(Beer beer);
		Task DeleteBeer(Beer beer);
	}
}