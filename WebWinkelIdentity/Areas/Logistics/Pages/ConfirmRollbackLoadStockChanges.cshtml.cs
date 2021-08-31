using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class ConfirmRollbackLoadStockChangesModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmRollbackLoadStockChangesModel(IMediator mediator)
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

        public IActionResult OnPost(int id)
        {
            var result = mediator.Send(new DeleteLoadStockChangeCommand(id)).Result;

            if (result.IsSuccess)
            {
                return LocalRedirect("/ProductsManagement/MultipleRollbacksIndex");
            }

            return LocalRedirect($"/ProductsManagement/LoadStockChangeDetail?id={id}");
        }
    }
}
