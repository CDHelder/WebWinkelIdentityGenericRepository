using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebWinkelIdentity.Core.StoreEntities
{
    public class LoadStockChange
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser AssociatedUser { get; set; }
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        public DateTime DateChanged { get; set; }
        public List<ProductStockChange> ProductStockChanges { get; set; }
        public string ExtraInfo { get; set; }

    }
}
