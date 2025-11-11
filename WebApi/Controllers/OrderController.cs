using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.In_Memory;
using WebApi.Models;
using WebApi.Models.Users;

namespace WebApi.Controllers
{

    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IRepository<Order> _repo;

        public OrderController(IRepository<Order> repo)
        {
            _repo = repo;
        }

        [Route("GetOrderListbyProduct/{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrderListbyProduct(Guid productId)
        {
            var orders = _repo.GetAll(o => productId == null || o.ProductId == productId);
            return Found(orders);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Order order)
        {
            if (order == null)
                return Found("order cannot be null");

            _repo.Create(order);
            return Found(order);
        }

        [Route("UpdateOrder/{id:guid}")]
        [HttpPut]
        public HttpResponseMessage UpdateOrder(Guid id, [FromBody] Order order)
        {
            order.Id = id;
            var updated = _repo.Update(order);
            if (updated == null) return Found("order id not found");
            return Found(updated);
        }


        [Route("DeleteOrder/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid id)
        {
            if (id == null)
                return Found("Order Guid cannot be null");
            var order = _repo.GetById(id);
            if(order == null)
            {
                return Found("Order does not found for " + id);
            }
            _repo.Delete(id);
            return Found("Deleted Order " + id);
        }



        [Route("GetOrders")]
        [HttpGet]
        public HttpResponseMessage GetOrders()
        {
            var order = _repo.GetAll();
            return Found(order);
        }

    }
}