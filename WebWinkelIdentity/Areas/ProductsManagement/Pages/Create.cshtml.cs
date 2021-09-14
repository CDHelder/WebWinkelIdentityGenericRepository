using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
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

            ViewData["BrandId"] = new SelectList(allBrandsAndCategories.Result.Value.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(allBrandsAndCategories.Result.Value.Categories, "Id", "Name");
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

            var result = mediator.Send(new CreateProductCommand(Product));

            if (result.Result.Value > 0)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={result.Result.Value}");
            }

            return Page();
        }
    }
}
