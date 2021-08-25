using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWinkelIdentity.Core.StoreEntities
{
    public class LoadStockChange
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser AssociatedUser { get; set; }
        public DateTime DateChanged { get; set; }
        public List<ProductStockChange> ProductStockChanges { get; set; }

        //TODO: Voeg property string explanation toe voor extra uitleg als bijv. admin uit meerdere steden producten uitboekt

    }
}
