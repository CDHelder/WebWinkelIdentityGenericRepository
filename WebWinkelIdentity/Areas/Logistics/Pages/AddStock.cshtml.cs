using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Web.Areas.Logistics.Pages
{
    public class AddStockModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public AddStockModel(IUnitOfWork unitOfWork)
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
            AllStores = unitOfWork.StoreRepository.GetList(
                include: store => store
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
            //TODO: check if list comes back correctly
            var list = AllText.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var store = unitOfWork.StoreRepository.GetById(int.Parse(SelectedStoreId));

            if (store == null)
            {
                FormResult = $"Error: Couldn't find store with id: {SelectedStoreId}";
            }

            foreach (var productId in list)
            {
                if (productId != "")
                {
                    var intId = int.Parse(productId);
                    var product = unitOfWork.ProductRepository.GetById(intId);
                    if (product == null)
                    {
                        FormResult = $"Error: Couldnt find product with id:{productId} in the database";
                        return Page();
                    }
                }
            }

            AllTextData = string.Join("\n",list);
            StoreId = int.Parse(SelectedStoreId);

            return RedirectToPage("/ConfirmAddStock");

        }
    }
}
