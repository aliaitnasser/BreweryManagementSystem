using Application.Core;
using Models;

namespace Application.Repositories
{
    public interface IOrderRepository
    {
         Task<Result<Order>> AddOrder(Order order);
    }
}