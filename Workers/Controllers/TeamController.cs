using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Workers.Models;

namespace Workers.Controllers
{
    public class TeamController : Controller
    {
        public IEFGenericRepository<Person> TeamsPersonrepository { get; set; }

        public TeamController(IEFGenericRepository<Person> teamspersonrepository)
        {
            TeamsPersonrepository = teamspersonrepository;
        }
        [Route("/{TeamName}/{TeamId}")]
        public IActionResult TeamIndex(Guid TeamId,string TeamName)
        {
            ViewData["Message"] = "Your Team page.";
            ViewData["TeamName"] = TeamName;
            return View(TeamsPersonrepository.Get(person=>person.TeamId== TeamId));
        }

    }
}
