﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Workers.ModelsView
{
    
    public class Person
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Days { get; set; }

        public Guid? TeamId { get; set; }

        /*    [DataMember]
            [ForeignKey("TeamId")]*/
        public virtual Team Team { get; set; }
        public List<Vacation> HolyDays { get; set; } = new List<Vacation>();
    }
}
