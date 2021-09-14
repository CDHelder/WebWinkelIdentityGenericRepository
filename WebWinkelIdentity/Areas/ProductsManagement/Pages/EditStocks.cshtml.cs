using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditStocksModel : PageModel
    {
        private readonly IMediator mediator;

        public EditStocksModel(IMediator mediator)
        {
            this.mediator = mediator;
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
            var allInfo = mediator.Send(new AllProductInformationQuery(id));

            if (allInfo.Result.Value.Product == null)
            {
                return NotFound();
            }

            Product = allInfo.Result.Value.Product;
            ProductVariations = allInfo.Result.Value.ProductVariations;
            ProductStocks = allInfo.Result.Value.ProductStocks;
            Stores = allInfo.Result.Value.Stores;

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

            var result = mediator.Send(new UpdateStoreProductsCommand(ProductStocks));

            if (result.Result.IsSuccess)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());

                var resultPSC = mediator.Send(new CreateAllProductStockChangesCommand(ProductStocks, beforeChangeStockValues, userId));

                if (resultPSC.Result.IsSuccess)
                    return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();

        }
    }
}
