using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class GlobalSetting
    {
        public Guid Id { get; set; }
        public string NameCompany { get; set; }
        public int VacationDays { get; set; }
    }
}
