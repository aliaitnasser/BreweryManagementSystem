using Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var result = await _orderRepository.AddOrder(order);
            return HandleResultWithMessage(result);
        }
    }
}