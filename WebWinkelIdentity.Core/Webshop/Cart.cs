using System.Collections.Generic;

namespace WebWinkelIdentity.Core
{
    public class Cart
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}
