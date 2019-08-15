using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Models_View
{
    public class NewVacationAdmin
    {
        public Guid Id;
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Days { get; set; }
        public Guid PersonId { get; set; }

    }
}
