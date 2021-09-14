using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Web.Application.Commands;
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class ConfirmDeliveryShipmentModel : PageModel
    {
        private readonly IMediator mediator;

        public ConfirmDeliveryShipmentModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public List<Shipment> Shipments { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }

        [TempData]
        public string FormResult { get; set; }
        [BindProperty]
        public string Ids { get; set; }

        public IActionResult OnGet(string ids)
        {
            if (ids == null)
            {
                return NotFound();
            }

            var intIds = ids.Split(", ").Select(Int32.Parse).ToList();
            var result = mediator.Send(new AllShipmentQuery(false, intIds)).Result;
            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }
            Shipments = result.Value;
            Ids = ids;

            var prodIds = Shipments.SelectMany(s => s.LoadStockChange.ProductStockChanges.Select(a => a.StoreProduct.ProductId)).Distinct().ToList();
            var storeId = Shipments.ElementAtOrDefault(0).StoreId;
            var resultStoreProducts = mediator.Send(new AllStoreProductsQuery(prodIds, storeId)).Result;
            if (resultStoreProducts.IsFailure)
            {
                FormResult = resultStoreProducts.Error;
                return Page();
            }
            StoreProducts = resultStoreProducts.Value;


            return Page();
        }

        public IActionResult OnPost(string ids)
        {

            var intIds = ids.Split(", ").Select(Int32.Parse).ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString());

            var result = mediator.Send(new ShipmentsDeliveryCommand(intIds, userId)).Result;

            if (result.IsFailure)
            {
                FormResult = result.Error;
                return Page();
            }

            return RedirectToPage("./HistoryIndex");
        }
    }
}
