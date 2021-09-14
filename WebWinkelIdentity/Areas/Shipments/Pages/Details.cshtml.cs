using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
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

            var resultShipment = mediator.Send(new ShipmentQuery(id)).Result;
            if (resultShipment.IsFailure)
            {
                FormResult = resultShipment.Error;
                return Page();
            }
            Shipment = resultShipment.Value;

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
    }
}
