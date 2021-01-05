using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
   public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}
