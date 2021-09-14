using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class ConfirmRollbackStockChangesModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmRollbackStockChangesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [BindProperty]
        public ProductStockChange ProductStockChange { get; set; }
        public bool RollBackIsPossible { get; set; }
        public int RollBackStockValue { get; set; }
        public int ProductId { get; set; }

        public void OnGet(int id)
        {
            ProductStockChange = mediator.Send(new ProductStockChangeAndCurrentStoreProductQuery(id)).Result.Value;
            RollBackStockValue = ProductStockChange.StoreProduct.Quantity + ProductStockChange.StockChange;

            if (RollBackStockValue < 0)
                RollBackIsPossible = false;
            else
                RollBackIsPossible = true;
        }

        public IActionResult OnPost()
        {
            ProductId = ProductStockChange.StoreProduct.ProductId;
            var result = mediator.Send(new ProductStockChangeRollBackCommand(ProductStockChange.Id));

            if (result.Result.IsSuccess)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={ProductId}");
            }

            return Page();
        }
    }
}
