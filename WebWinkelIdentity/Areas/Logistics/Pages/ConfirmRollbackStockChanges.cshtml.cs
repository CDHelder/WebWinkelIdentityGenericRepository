using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
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

        public void OnGet(int id)
        {
            ProductStockChange = mediator.Send(new ProductStockChangeAndCurrentStoreProductQuery(id)).Result.Value;
            RollBackStockValue = ProductStockChange.StoreProduct.Quantity + ProductStockChange.StockChange;

            if (RollBackStockValue < 0)
                RollBackIsPossible = false;
            else
                RollBackIsPossible = true;
        }

        public void OnPost()
        {
            //TODO: Extra functie die controleert dat rollback kan
            //TODO: Mediator CQRS Command voor RollBack
            //TODO: if Result.IsSuccesfull -> RedirectToPage("/SuccesfullyRollbackedStockChanges");
        }
    }
}
