using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Workers.ModelsView
{ 
    public class VacationView
    {

        [Required]
        public string startDay { get; set; }
        [Required]
        public string EndDay { get; set; }
    }
}
