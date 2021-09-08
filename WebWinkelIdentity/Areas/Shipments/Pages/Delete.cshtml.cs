using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator mediator;

        public DeleteModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Shipment Shipment { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
        public string FormResult { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = mediator.Send(new ShipmentQuery(id)).Result;
            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }
            Shipment = result.Value;

            var prodIds = Shipment.LoadStockChange.ProductStockChanges.Select(a => a.StoreProduct.ProductId).ToList();
            var resultStoreProducts = mediator.Send(new AllStoreProductsQuery(prodIds, Shipment.StoreId)).Result;
            if (resultStoreProducts.IsFailure)
            {
                FormResult = resultStoreProducts.Error;
                return Page();
            }
            StoreProducts = resultStoreProducts.Value;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //TODO: Maak DeleteShipmentCommand(int id)
            var result = mediator.Send(new DeleteShipmentCommand(id)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
