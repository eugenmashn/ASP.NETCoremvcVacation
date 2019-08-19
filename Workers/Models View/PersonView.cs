using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Models_View
{
    public class PersonView
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
    }
}
