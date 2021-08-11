using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            ViewData["BrandId"] = new SelectList(unitOfWork.BrandRepository.GetAll(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(unitOfWork.CategoryRepository.GetAll(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.ProductRepository.Create(Product);

            if (unitOfWork.SaveChanges() == true)
            {
                return RedirectToPage($"./Details/{Product.Id}");
            }

            return Page();
        }
    }
}
