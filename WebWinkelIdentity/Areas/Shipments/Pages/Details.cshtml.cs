using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WebWinkelIdentity.Data.ApplicationDbContext _context;

        public DetailsModel(WebWinkelIdentity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Shipment Shipment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shipment = await _context.Shipments
                .Include(s => s.EndLocationStore)
                .Include(s => s.LoadStockChange).FirstOrDefaultAsync(m => m.Id == id);

            if (Shipment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
