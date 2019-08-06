using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Workers.Models;
using Microsoft.AspNetCore.Authorization;
namespace Workers.Controllers
{

    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        public IEFGenericRepository<Team> Teamrepository { get; set; }
        public IEFGenericRepository<Person> PersonRepository { get; set; }

        public HomeController(IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Person> personrepository)
        {
            Teamrepository = teamrepository;
            PersonRepository = personrepository;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<Team> Teams = Teamrepository.Get().ToList();

            return View(Teams);
        }
        [Route("[controller]/[action]")]
        /* [Route("/Workers")]*/
        public IActionResult Workers()
        {

            ViewData["Message"] = "Your application description page.";
            ViewBag.Teams = Teamrepository.Get().ToList();

            return View(PersonRepository.IncludeGet(p => p.Team));
        }
        /*[Route("/{TeamName}")]
        public IActionResult TeamIndex()
        {
            ViewData["Message"] = "Your Team page.";

            return View();
        }*/
        /*        [Authorize]*/
        // [Authorize(Roles = "admin")]
        [Route("[controller]/[action]")]
        /*    [Route("/AddnewTeam")]*/
        public IActionResult AddnewTeam()
        {
            ViewData["Message"] = "Add Team  page.";

            return View();
        }
        /*       [Authorize]*/
        /* [Route("/CreatenewTeam")]*/
        // [Authorize(Roles = "admin")]
        [Route("[controller]/[action]")]
        [HttpPost]
        public IActionResult CreatenewTeam(Team newTeam)
        {
            Team team = new Team();
            team.MinNumberWorkers = newTeam.MinNumberWorkers;
            team.TeamName = newTeam.TeamName;
            team.Id = Guid.NewGuid();
            if (team.MinNumberWorkers > 20 || team.MinNumberWorkers < 0)
                return Redirect("~/CreatenewTeam");
            Teamrepository.Create(newTeam);

            return Redirect("~/");
        }
        //  [Authorize]
        [Route("[controller]/[action]")]
        /* [Route("/Contact")]*/
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }
        [Route("[controller]/[action]")]
        public IActionResult Privacy()
        {
            return View();
        }
        /*        [Authorize]*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles ="admin")]
        [Route("[controller]/[action]")]
        public IActionResult NextYear()
        {
            List<Person> people = PersonRepository.IncludeGet(p => p.Team).ToList();
            foreach (Person person in people)
            {
                Person newperson = person;
                newperson.Days += 18;
                PersonRepository.Update(newperson);
            }
            return Redirect("/Home/Workers/Home/Workers");
        }

    }
}
