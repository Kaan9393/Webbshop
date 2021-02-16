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
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public IndexModel(ILogger<IndexModel> logger, IUserRepository userRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public List<Product> AllProducts { get; set; }

        public void OnGet()
        {
            _userRepository.CheckForAdmin();
            AllProducts = _productRepository.GetAllProducts().ToList();
        }
    }
}
