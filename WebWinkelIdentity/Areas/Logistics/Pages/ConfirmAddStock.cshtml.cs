using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class ConfirmAddStockModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public ConfirmAddStockModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public List<StoreProduct> StoreProducts { get; set; }
        [BindProperty]
        public List<int> AllTextDataList { get; set; }
        [BindProperty]
        public int PostStoreId { get; set; }

        [TempData]
        public int StoreId { get; set; }
        [TempData]
        public int SuccesStoreId { get; set; }
        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string AllTextData { get; set; }

        //TODO: Check if new Repository works correctly
        public IActionResult OnGet()
        {
            var AllTextDataArray = AllTextData.Split("\n");
            Array.Sort(AllTextDataArray);

            AllTextDataList = AllTextDataArray.Select(x => int.Parse(x)).ToList();
            StoreProducts = unitOfWork.StoreProductRepository.GetAllStoreProducts(AllTextDataList, StoreId);
            PostStoreId = StoreId;

            return Page();
        }

        public IActionResult OnPost()
        {
            foreach (var storeProduct in StoreProducts)
            {
                var addQuantity = AllTextDataList.Where(x=>x==storeProduct.ProductId).Count();
                storeProduct.Quantity += addQuantity;

                unitOfWork.StoreProductRepository.Update(storeProduct);
                if (unitOfWork.SaveChanges() == false)
                {
                    FormResult = $"Error: Couldnt save product with id:{storeProduct.ProductId} in the database";
                    return Page();
                }

                ProductStockChange PSC = new ProductStockChange
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()),
                    DateChanged = DateTime.Now,
                    StoreProductId = storeProduct.Id,
                    StockChange = addQuantity
                };

                unitOfWork.ProductStockChangeRepository.Create(PSC);
                if (unitOfWork.SaveChanges() == false)
                {
                    FormResult = $"Error: Couldnt log the stock changes made to product with id:{storeProduct.ProductId}";
                    return Page();
                }
            }

            AllTextData = string.Join("\n", AllTextDataList);
            SuccesStoreId = PostStoreId;
            return RedirectToPage("/SuccesfullyAddedStock");
        }
    }
}
