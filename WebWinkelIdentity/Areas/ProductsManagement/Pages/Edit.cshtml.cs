using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
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

            if (allInfo.Result.Value.Product == null)
            {
                return NotFound();
            }

            Product = allInfo.Result.Value.Product;
            ProductVariations = allInfo.Result.Value.ProductVariations;

            var allBrandsAndCategories = mediator.Send(new AllBrandsAndCategoriesQuery());
            ViewData["BrandId"] = new SelectList(allBrandsAndCategories.Result.Value.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(allBrandsAndCategories.Result.Value.Categories, "Id", "Name");

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = mediator.Send(new UpdateProductVariationsCommand(Product, ProductVariations));

            if (result.Result.IsSuccess)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();
        }
    }
}
