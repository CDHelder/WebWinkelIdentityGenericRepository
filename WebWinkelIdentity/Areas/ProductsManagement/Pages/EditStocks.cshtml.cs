using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        [BindProperty]
        public List<StoreProduct> ProductStocks { get; set; }

        public Dictionary<int, int> BeforeChangeStockValues { get; set; }
        public List<Store> Stores { get; set; }
        public bool CurrentStock { get; set; }

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
            Stores = unitOfWork.StoreRepository.GetAll(include: store => store.Include(s => s.Address));
            BeforeChangeStockValues = new();

            CurrentStock = false;
            foreach (var productStock in ProductStocks)
            {
                BeforeChangeStockValues.Add(productStock.Id,productStock.Quantity);
                if (productStock.Quantity > 0)
                {
                    CurrentStock = true;
                }
            }

            return Page();
        }

        public IActionResult OnPostAsync(Dictionary<int, int> beforeChangeStockValues)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.StoreProductRepository.Update(ProductStocks);

            if (unitOfWork.SaveChanges() == true)
            {
                CreateAndSaveProductStockChanges(beforeChangeStockValues);

                if (unitOfWork.SaveChanges() == true)
                    return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();

        }

        private void CreateAndSaveProductStockChanges(Dictionary<int, int> beforeChangeStockValues)
        {
            foreach (var storeProduct in ProductStocks)
            {
                if (storeProduct.Quantity - beforeChangeStockValues[storeProduct.Id] != 0)
                {
                    var PSC = new ProductStockChange
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()),
                        DateChanged = DateTime.Now,
                        StockChange = storeProduct.Quantity - beforeChangeStockValues[storeProduct.Id],
                        StoreProductId = storeProduct.Id
                    };

                    unitOfWork.ProductStockChangeRepository.Create(PSC);
                }
            }
        }
    }
}
