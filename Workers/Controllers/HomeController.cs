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
        public IEFGenericRepository<Team> TeamRepository { get; set; }
        public IEFGenericRepository<Person> PersonRepository { get; set; }
        public IEFGenericRepository<HistoryAddingDays> HistoryAddingDaysRepository { get; set; }

        public IEFGenericRepository<Weekend> WeekendRepository { get; set; }
        public HomeController(IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Person> personrepository,IEFGenericRepository<HistoryAddingDays>historyAddingDaysrepository,IEFGenericRepository<Weekend> wekendRepository)
        {
            TeamRepository = teamrepository;
            PersonRepository = personrepository;
            HistoryAddingDaysRepository = historyAddingDaysrepository;
            WeekendRepository = wekendRepository;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<Team> Teams = TeamRepository.Get().ToList();

            return View(Teams);
        }
        [Route("[controller]/[action]")]
        /* [Route("/Workers")]*/
        public IActionResult Workers()
        {

            ViewData["Message"] = "Your application description page.";
            ViewBag.Teams = TeamRepository.Get().ToList();
            List<Person> people = PersonRepository.Get().ToList();
            if (people.Count == 0)
                ViewBag.NextYear = false;
            else {
                if (HistoryAddingDaysRepository.Get().Count() == 0)
                {
                    ViewBag.NextYear = true;
                }
                else if (HistoryAddingDaysRepository.Get(p => p.Year == people[0].Year ) == null)
                {
                    ViewBag.NextYear = true;
                }
                else {
                    ViewBag.NextYear = false;
                }
            }
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
            TeamRepository.Create(newTeam);

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
            HistoryAddingDays historyAddingDays = new HistoryAddingDays();
            if (people[0] == null)
                return Redirect("/Home/Workers/Home/Workers");
            historyAddingDays.Year = people[0].Year;
            historyAddingDays.NumberAddDays = 17;
            historyAddingDays.Id = Guid.NewGuid();
            historyAddingDays.CheckAddDays = true;
            HistoryAddingDaysRepository.Create(historyAddingDays);
            foreach (Person person in people)
            {
                Person newperson = person;
                person.Year ++;
                newperson.Days += 18;
                PersonRepository.Update(newperson);
            }
            List<Weekend> weekends = WeekendRepository.Get(p => p.startDate.Year == DateTime.Now.Year).ToList();
            List<Weekend> NewWeekends = new List<Weekend>();
            foreach (Weekend weekend in weekends)
            {         
               weekend.startDate= weekend.startDate.AddYears(1);
               weekend.EndDate  =weekend.EndDate.AddYears(1);
                WeekendRepository.Update(weekend);
                NewWeekends.Add(weekend);
            }
             
            return View(NewWeekends);
        }

    }
}
