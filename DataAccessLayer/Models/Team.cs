using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        //  public int Year { get; set; }
        public int MinNumberWorkers { get; set; }
        public  List<Person> Workers { get; set; }
    }
}
