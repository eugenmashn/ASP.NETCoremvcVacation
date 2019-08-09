using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Workers.Models_View
{
    public class Holydays
    {
        
        public string Name { get; set; }
        [DisplayName("startDay")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string startDay { get; set; }
        [DefaultValue(1)]
        public int  AddDays{ get; set; }
        
    }
}
