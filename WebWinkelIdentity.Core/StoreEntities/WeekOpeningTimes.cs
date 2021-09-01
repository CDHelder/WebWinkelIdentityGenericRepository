using System;
using System.Collections.Generic;

namespace WebWinkelIdentity.Core.StoreEntities
{
    public class WeekOpeningTimes
    {
        public int Id { get; set; }
        public TimeSpan MondayOpeningTime { get; set; }
        public TimeSpan MondayClosingTime { get; set; }
        public TimeSpan TuesdayOpeningTime { get; set; }
        public TimeSpan TuesdayClosingTime { get; set; }
        public TimeSpan WednesdayOpeningTime { get; set; }
        public TimeSpan WednesdayClosingTime { get; set; }
        public TimeSpan ThursdayOpeningTime { get; set; }
        public TimeSpan ThursdayClosingTime { get; set; }
        public TimeSpan FridayOpeningTime { get; set; }
        public TimeSpan FridayClosingTime { get; set; }
        public TimeSpan SaturdayOpeningTime { get; set; }
        public TimeSpan SaturdayClosingTime { get; set; }
        public TimeSpan SundayOpeningTime { get; set; }
        public TimeSpan SundayClosingTime { get; set; }
    }
}
