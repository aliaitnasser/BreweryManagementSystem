using Models;

namespace Application.Repositories
{
    public interface IOrderRepository
    {
         Task AddOrder(Order order);
    }
}