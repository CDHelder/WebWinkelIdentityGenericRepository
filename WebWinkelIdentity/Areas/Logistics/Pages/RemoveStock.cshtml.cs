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

        //TODO: Check if new Repository works correctly
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
            var list = AllText.Split("\n");

            var store = unitOfWork.StoreRepository.GetById(int.Parse(SelectedStoreId));
            if (store == null)
            {
                FormResult = $"Error: Couldn't find store with id: {SelectedStoreId}";
            }

            foreach (var productId in list)
            {
                var product = unitOfWork.ProductRepository.GetById(int.Parse(productId));
                if (product == null)
                {
                    FormResult = $"Error: Couldnt find product with id:{productId} in the database";
                    return Page();
                }
            }

            AllTextData = AllText;
            StoreId = int.Parse(SelectedStoreId);

            return RedirectToPage("/ConfirmRemoveStock");

        }
    }
}
