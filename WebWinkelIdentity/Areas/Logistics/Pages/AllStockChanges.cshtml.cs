using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class AllStockChangesModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public AllStockChangesModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //TODO: Maak rollback functie om gemaakte veranderingen terug te veranderen
        public List<ProductStockChange> ProductStockChange { get;set; }

        public void OnGetAsync()
        {
            ProductStockChange = unitOfWork.ProductStockChangeRepository.GetAllProductStockChangesAndIncludes();
        }
    }
}
