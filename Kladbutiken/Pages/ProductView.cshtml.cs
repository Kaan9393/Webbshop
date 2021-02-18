using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kladbutiken.Pages
{
    public class ProductViewModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        [BindProperty (SupportsGet = true)]
        public Guid SelectedProduct { get; set; }

        public Product Product { get; set; }

        //public double PriceWithDiscount { get; set; }

        public List<Product> MatchingProducts { get; set; }

        public ProductViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            Product = _productRepository.GetProductById(SelectedProduct);

            MatchingProducts = _productRepository.GetProductsByCategory(Product.Category.TypeName).ToList();
            foreach (var matchedProduct in MatchingProducts)
            {
                matchedProduct.PriceWithDiscount = _productRepository.GetPriceWithDiscount(matchedProduct.Price,matchedProduct.Discount);

            }

            Product.PriceWithDiscount = _productRepository.GetPriceWithDiscount(Product.Price, Product.Discount);
        }
    }
}
