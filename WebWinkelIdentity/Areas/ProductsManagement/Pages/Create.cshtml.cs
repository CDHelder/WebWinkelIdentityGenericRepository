﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IMediator mediator;

        public CreateModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult OnGet()
        {
            var allBrandsAndCategories = mediator.Send(new AllBrandsAndCategoriesQuery());

            ViewData["BrandId"] = new SelectList(allBrandsAndCategories.Result.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(allBrandsAndCategories.Result.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TODO: Maak Create Command (mediator)
            var result = mediator.Send(new CreateProductCommand(Product));

            if (result.Result == true)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();
        }
    }
}
