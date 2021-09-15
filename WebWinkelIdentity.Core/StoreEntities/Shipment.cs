using Microsoft.AspNetCore.Identity;
using System;

namespace WebWinkelIdentity.Core.StoreEntities
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime? DeliveredTime { get; set; }
        public string UserId { get; set; }
        public IdentityUser UserThatConfirmed { get; set; }
        public bool Delivered { get; set; }
        public int StoreId { get; set; }
        public Store EndLocationStore { get; set; }
        public int LoadStockChangeId { get; set; }
        public LoadStockChange LoadStockChange { get; set; }
    }
}
