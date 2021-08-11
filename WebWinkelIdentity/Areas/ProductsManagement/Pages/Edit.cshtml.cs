using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<Product> ProductVariations { get; set; }

        //TODO: Check if new Repository works correctly
        //TODO: Fix decimal input mag ook decimale waardes geven en niet alleen hele
        //VB = Input: 15,95 Veranderd naar: 1595
        public IActionResult OnGetAsync(int id)
        {
            Product = unitOfWork.ProductRepository.GetById(id);

            if (Product == null)
            {
                return NotFound();
            }

            ProductVariations = unitOfWork.ProductRepository.GetProductVariations(Product);

            ViewData["BrandId"] = new SelectList(unitOfWork.BrandRepository.GetAll(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(unitOfWork.CategoryRepository.GetAll(), "Id", "Name");

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.ProductRepository.UpdateProductProperties(Product, ProductVariations);

            if (unitOfWork.SaveChanges() == true)
            {
                return LocalRedirect($"/ProductsManagement/Details?id={Product.Id}");
            }

            return Page();
        }
    }
}
