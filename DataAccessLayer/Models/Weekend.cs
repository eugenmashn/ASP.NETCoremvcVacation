using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Weekend
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
