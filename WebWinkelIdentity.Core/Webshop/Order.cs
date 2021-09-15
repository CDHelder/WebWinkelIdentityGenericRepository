﻿using System.Collections.Generic;

namespace WebWinkelIdentity.Core
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public bool IsDelivered { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
