﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMediator mediator;

        public EditModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<Product> ProductVariations { get; set; }

        //TODO: Fix decimal input mag ook decimale waardes geven en niet alleen hele
        //VB = Input: 15,95 Veranderd naar: 1595
        public IActionResult OnGetAsync(int id)
        {
            var allInfo = mediator.Send(new AllProductInformationQuery(id));

            if (allInfo.Result.Product == null)
            {
                return NotFound();
            }

            Product = allInfo.Result.Product;
            ProductVariations = allInfo.Result.ProductVariations;

            //TODO: Create AllBrandAndCategoryQuery (Mediator)
            ViewData["BrandId"] = new SelectList(unitOfWork.BrandRepository.GetAll(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(unitOfWork.CategoryRepository.GetAll(), "Id", "Name");

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Create UpdateAllProductVariantsCommand (MediatoR)
            unitOfWork.ProductRepository.UpdateProductProperties(Product, ProductVariations);

            if (unitOfWork.SaveChanges() == true)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();
        }
    }
}
