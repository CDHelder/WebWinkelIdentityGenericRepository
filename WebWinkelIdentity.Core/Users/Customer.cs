using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebWinkelIdentity.Core
{
    public class Customer : IdentityUser
    {
        public string Name { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Order> Orders { get; set; }
    }
}
