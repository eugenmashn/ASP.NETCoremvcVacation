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
    public class HomeController : Controller
    {
        public IEFGenericRepository<Team> Teamrepository { get; set; }
        public IEFGenericRepository<Person> PersonRepository { get; set; }
        public HomeController(IEFGenericRepository<Team> teamrepository,IEFGenericRepository<Person>personrepository)
        {
            Teamrepository = teamrepository;
            PersonRepository = personrepository;
        }

        public IActionResult Index()
        {
           List<Team> Teams= Teamrepository.Get().ToList();

            return View(Teams);
        }

        [Route("/Workers")]
        public IActionResult Workers()
        {
        
            ViewData["Message"] = "Your application description page.";
            ViewBag.Teams= Teamrepository.Get().ToList();
            
            return View(PersonRepository.IncludeGet(p=>p.Team));
        }
        /*[Route("/{TeamName}")]
        public IActionResult TeamIndex()
        {
            ViewData["Message"] = "Your Team page.";

            return View();
        }*/
       
   
           [Route("/AddnewTeam")]
        public IActionResult AddnewTeam()
        {
            ViewData["Message"] = "Add Team  page.";

            return View();
        }
       [Route("/CreatenewTeam")]
       [HttpPost]
        public IActionResult CreatenewTeam(Team newTeam)
        {
            Team team = new Team();
            team.MinNumberWorkers = newTeam.MinNumberWorkers;
            team.TeamName = newTeam.TeamName;
            team.Id = Guid.NewGuid();
            Teamrepository.Create(newTeam);
            return Redirect("~/");
        }
        [Route("/Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
