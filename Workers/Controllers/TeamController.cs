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
        public IEFGenericRepository<Team> TeamRepository { get; set;}

        public IEFGenericRepository<Person> Personrepository { get; set; }

        public TeamController(IEFGenericRepository<Person> personrepository,IEFGenericRepository<Team> teamRepository)
        {
            Personrepository = personrepository;
            TeamRepository = teamRepository;

        }
        [Route("/{TeamName}/{TeamId}")]
        public IActionResult TeamIndex(Guid TeamId,string TeamName)
        {
            ViewData["Message"] = "Your Team page.";
            ViewData["TeamName"] = TeamName;
            ViewBag.TeamId = TeamId;
            return View(Personrepository.Get(person=>person.TeamId== TeamId));
        }
        [Route("{TeamName}")]
        public IActionResult TeamDelete(Guid TeamId)
        {
            Team team = TeamRepository.FindById(x=>x.Id==TeamId);
            TeamRepository.Remove(team);
             return Redirect("~/");
        }
    }
}
