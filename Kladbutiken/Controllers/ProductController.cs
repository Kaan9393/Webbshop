using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Kladbutiken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Route("cart")]
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<List<Product>> GetProductCart(List<Guid> cart)
        {
            return _productRepository.GetProductsByList(cart);
        }
    }
}
