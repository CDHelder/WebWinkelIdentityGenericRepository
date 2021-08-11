using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data.Service.Interfaces;

namespace WebWinkelIdentity.Areas.ProductsManagement.Pages
{
    [Authorize(Roles = "Admin,Employee")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Product> Product { get;set; }

        //TODO: Check if new Repository works correctly
        public void OnGetAsync()
        {
            Product = unitOfWork.ProductRepository.GetList(include: store => store
                .Include(s => s.Brand)
                .Include(s => s.Category));
        }
    }
}
