﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Product Product { get; set; }
        public List<Product> ProductVariations { get; set; }
        public List<StoreProduct> ProductStocks { get; set; }
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
    }
}
