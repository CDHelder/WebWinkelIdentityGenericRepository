using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class RemoveStockModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveStockModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public string AllText { get; set; }
        [BindProperty]
        public string SelectedStoreId { get; set; }
        public List<SelectListItem> AllStores { get; set; }

        [TempData]
        public int StoreId { get; set; }
        [TempData]
        public string FormResult { get; set; }
        [TempData]
        public string AllTextData { get; set; }

        public IActionResult OnGet()
        {
            AllText = null;
            AllStores = unitOfWork.StoreRepository.GetList(include: sp => sp
                .Include(s => s.Address))
                .Select(s =>
            new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Address.City
            }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (AllText == null)
            {
                FormResult = "Please enter a product id";
                return Page();
            }
            if (SelectedStoreId == null)
            {
                FormResult = "Please select a store";
                return Page();
            }

            AllText = AllText.Replace("\r", "");
            var list = AllText.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (DoesStoreExcist() == false)
            {
                FormResult = $"Error: Couldn't find store with id: {SelectedStoreId}";
                return Page();
            }

            foreach (var productId in list.Distinct())
            {
                if (DoesProductExcist(productId) == false)
                {
                    FormResult = $"Error: Couldnt find product with id:{productId} in the database";
                    return Page();
                }
            }

            //TODO: Check if DoesEnteredStockChangeExcist function works correctly
            if (DoesEnteredStockChangeExcist(list) == false)
            {
                return Page();
            }

            AllTextData = AllText;
            StoreId = int.Parse(SelectedStoreId);

            return RedirectToPage("/ConfirmRemoveStock");

        }

        private bool DoesEnteredStockChangeExcist(string[] list)
        {
            var intList = list.Select(x => int.Parse(x)).ToList();
            foreach (var id in unitOfWork.StoreProductRepository.GetAllStoreProducts(intList, int.Parse(SelectedStoreId)))
            {
                var enteredStockChange = intList.Where(adat => adat == id.ProductId).Count();
                var currentStock = id.Quantity;
                if (currentStock < enteredStockChange)
                {
                    FormResult = $"Error: Cant delete {enteredStockChange} products with id:{id.ProductId} " +
                        $"because current stock is {currentStock}";
                    return false;
                }
            }
            return true;
        }

        private bool DoesProductExcist(string productId)
        {
            var product = unitOfWork.ProductRepository.GetById(int.Parse(productId));
            if (product == null)
            {
                return false;
            }
            return true;
        }

        private bool DoesStoreExcist()
        {
            var store = unitOfWork.StoreRepository.GetById(int.Parse(SelectedStoreId));
            if (store == null)
            {
                return false;
            }
            return true;
        }
    }
}
