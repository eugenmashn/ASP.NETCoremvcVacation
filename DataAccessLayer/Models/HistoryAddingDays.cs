using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class HistoryAddingDays
    {
        public Guid Id { get; set; }
        public bool CheckAddDays{ get; set; }
        public int NumberAddDays { get; set; }
        public int Year { get; set; }
        
    }
}
