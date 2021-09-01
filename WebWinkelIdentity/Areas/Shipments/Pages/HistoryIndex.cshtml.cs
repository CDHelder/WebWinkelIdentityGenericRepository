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
    public class HistoryIndexModel : PageModel
    {
        private readonly WebWinkelIdentity.Data.ApplicationDbContext _context;

        public HistoryIndexModel(WebWinkelIdentity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Shipment> Shipment { get;set; }

        public async Task OnGetAsync()
        {
            Shipment = await _context.Shipments
                .Include(s => s.EndLocationStore)
                .Include(s => s.LoadStockChange).ToListAsync();
        }
    }
}
