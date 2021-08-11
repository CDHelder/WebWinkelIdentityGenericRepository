﻿using System.Collections.Generic;

namespace WebWinkelIdentity.Core.StoreEntities
{
    public class Store
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<StoreEmployee> StoreEmployees { get; set; }
        public int? WeekOpeningTimesId { get; set; }
        public WeekOpeningTimes WeekOpeningTimes { get; set; }
    }
}
