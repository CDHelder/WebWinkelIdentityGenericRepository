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
    [Authorize(Roles = "Admin")]
    public class EditStocksModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditStocksModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<Product> ProductVariations { get; set; }

        public List<StoreProduct> ProductStocks { get; set; }
        public List<Store> Stores { get; set; }
        public bool CurrentStock { get; set; }

        //TODO: Check if new Repository works correctly
        public IActionResult OnGetAsync(int id)
        {
            Product = unitOfWork.ProductRepository.Get(
                filter: p => p.Id == id,
                include: product => product
                    .Include(p => p.Brand)
                    .Include(p => p.Category));

            if (Product == null)
            {
                return NotFound();
            }

            ProductVariations = unitOfWork.ProductRepository.GetProductVariations(Product);
            ProductStocks = unitOfWork.StoreProductRepository.GetAllStoreProducts(ProductVariations);
            Stores = unitOfWork.StoreRepository.GetList(include: store => store.Include(s => s.Address));

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

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.StoreProductRepository.Update(ProductStocks);

            if(unitOfWork.SaveChanges() == true)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();

        }
    }
}
