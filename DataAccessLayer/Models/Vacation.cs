using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Vacation
    {
        public Guid Id { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime SecontDate { get; set; }
        public int Days { get; set; }
        public Person People { get; set; }
        public string TeamName { get; set; }
        public bool IndexDate { get; set; }
        public Guid Peopleid { get; set; }
    }
}
