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

        public List<Product> AllProduct { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IUserRepository userRepository, IProductRepository productRepository )
        {
            _logger = logger;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            _userRepository.CheckForAdmin();
            AllProduct = _productRepository.GetAllProducts().ToList();
        }
    }
}
