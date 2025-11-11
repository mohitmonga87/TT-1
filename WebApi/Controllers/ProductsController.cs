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
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {

        private readonly IRepository<Product> _repo;

        public ProductsController(IRepository<Product> repo)
        {
            _repo = repo;
        }


        [Route("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetList()
        {
            var products = _repo.GetAll();
            return Found(products);
        }

        [Route("CreateProduct")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Product prod)
        {
            if (prod == null)
                return Found("Product cannot be null");

            _repo.Create(prod);
            return Found(prod);
        }

        [Route("UpdateProduct")]
        [HttpPut]
        public HttpResponseMessage UpdateProduct(Product prod)
        {
            if (prod == null)
                return Found("Product cannot be null");

           _repo.Update(prod);
            return Found(prod);
        }


        [Route("DeleteProduct/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid id)
        {
            if (id == null)
                return Found("Product Guid cannot be null");
            var product = _repo.GetById(id);
            if(product == null)
            {
                return Found("Product does not found for " + id);
            }
            _repo.Delete(id);
            return Found("Deleted Product "+id);
        }



        [Route("GetProductbyId/{id:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProductbyId(Guid id)
        {
            var product = _repo.GetById(id);
            return Found(product);
        }

    }
}