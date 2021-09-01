using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWinkelIdentity.Core.StoreEntities;
using WebWinkelIdentity.Data;

namespace WebWinkelIdentity.Web.Areas.Shipments.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WebWinkelIdentity.Data.ApplicationDbContext _context;

        public CreateModel(WebWinkelIdentity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
        ViewData["LoadStockChangeId"] = new SelectList(_context.LoadStockChanges, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Shipment Shipment { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Shipments.Add(Shipment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
