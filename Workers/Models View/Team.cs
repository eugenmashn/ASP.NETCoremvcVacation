﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Workers.ModelsView
{
    public class Team
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        //  public int Year { get; set; }
        public int MinNumberWorkers { get; set; }
        public virtual List<Person> Workers { get; set; }
    }
}
