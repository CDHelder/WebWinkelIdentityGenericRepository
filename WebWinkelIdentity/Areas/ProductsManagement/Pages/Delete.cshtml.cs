using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Product> ProductVariations { get; set; }
        public List<StoreProduct> ProductStocks { get; set; }
        public List<Store> Stores { get; set; }
        public bool CurrentStock { get; set; }

        public IActionResult OnGetAsync(int id)
        {
            var allInfo = mediator.Send(new AllProductInformationQuery(id));

            if (allInfo.Result.Product == null)
            {
                return NotFound();
            }

            Product = allInfo.Result.Product;
            ProductVariations = allInfo.Result.ProductVariations;
            ProductStocks = allInfo.Result.ProductStocks;
            Stores = allInfo.Result.Stores;

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

        public async Task<ActionResult> OnPostAsync(int id)
        {
            //TODO: Maak de delete functie zo dat ie ook de productvariants verwijderd
            var response = await mediator.Send(new ProductDeleteCommand(id));

            if (response)
                return RedirectToPage("./Index");

            return Page();
        }
    }
}
