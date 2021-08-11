using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Product> ProductVariations { get; set; }
        public List<StoreProduct> ProductStocks { get; set; }
        public List<Store> Stores { get; set; }
        public bool CurrentStock { get; set; }

        public IActionResult OnGetAsync(int id)
        {
            Product = unitOfWork.ProductRepository.GetById(id);

            if (Product == null)
            {
                return NotFound();
            }

            ProductVariations = unitOfWork.ProductRepository.GetProductVariations(Product);
            ProductStocks = unitOfWork.StoreProductRepository.GetAllStoreProducts(ProductVariations);
            Stores = unitOfWork.StoreRepository.GetList(include: store => store.Include( s => s.Address));

            CurrentStock = false;
            foreach (var productStock in ProductStocks)
            {
                if (productStock.Quantity > 0)
                {
                    CurrentStock = true;
                }
            }

            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            var product = unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            unitOfWork.ProductRepository.Delete(id);

            if (unitOfWork.SaveChanges() == true)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
