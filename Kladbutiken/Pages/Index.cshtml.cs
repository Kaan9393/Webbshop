using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DataAccess.Repositories;
using DataAccess.Entities;

namespace Kladbutiken.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //private readonly IProductRepository _productRepository;
        private IEnumerable<Product> products;

        public IndexModel(ILogger<IndexModel> logger/*, IProductRepository productRepository*/)
        {
            _logger = logger;
            //_productRepository = productRepository;
        }

        public void OnGet()
        {
            //products = _productRepository.GetProductsByCategory("");
        }
    }
}
