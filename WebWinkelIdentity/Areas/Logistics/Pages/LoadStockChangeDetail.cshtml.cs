using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Queries;
using Microsoft.AspNetCore.Authorization;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class LoadStockChangeDetailModel : PageModel
    {
        private readonly IMediator mediator;

        public LoadStockChangeDetailModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public LoadStockChange LoadStockChange { get; set; }
        public bool RollbackPossible { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int id)
        {
            RollbackPossible = true;

            var result = mediator.Send(new LoadStockChangeQuery(id)).Result;

            if (result.IsFailure)
                ErrorMessage = result.Error;
            else if (result.IsSuccess)
                LoadStockChange = result.Value;

            foreach (var PSC in LoadStockChange.ProductStockChanges)
            {
                if (PSC.StoreProduct.Quantity + PSC.StockChange < 0)
                {
                    RollbackPossible = false;
                }
            }
        }
    }
}
