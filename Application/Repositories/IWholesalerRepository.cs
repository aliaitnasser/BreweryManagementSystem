using Models;

using System;
using System.Linq;

namespace Application.Repositories
{
	public interface IWholesalerRepository
	{
		Task AddSale(WholesalerBeer wholesalerBeer);
		Task UpdateRemainingStock(int wholesalerBeerId, int stock);
	}
}
