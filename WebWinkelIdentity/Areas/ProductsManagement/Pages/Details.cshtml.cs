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
using WebWinkelIdentity.Web.Application.Queries;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Product Product { get; set; }
        public List<Product> ProductVariations { get; set; }
        public List<StoreProduct> ProductStocks { get; set; }
        public List<Store> Stores { get; set; }
        public bool CurrentStock { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
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
    }
}
