using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class SuccesfullyAddedStockModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public SuccesfullyAddedStockModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public List<StoreProduct> StoreProducts { get; set; }

        [TempData]
        public string AllTextData { get; set; }
        [TempData]
        public int SuccesStoreId { get; set; }

        public List<int> AllTextDataList { get; set; }

        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);

            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();
            StoreProducts = unitOfWork.StoreProductRepository.GetAllStoreProducts(AllTextDataList, SuccesStoreId);

            return Page();
        }

    }
}
