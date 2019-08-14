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
using Workers.Models_View;
using System.Globalization;
using DataAccessLayer;
namespace Workers.Controllers
{

    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        public IEFGenericRepository<Team> TeamRepository { get; set; }
        public IEFGenericRepository<Person> PersonRepository { get; set; }
        public IEFGenericRepository<HistoryAddingDays> HistoryAddingDaysRepository { get; set; }
        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }

        public IEFGenericRepository<Weekend> WeekendRepository { get; set; }
        public HomeController(IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Person> personrepository,IEFGenericRepository<HistoryAddingDays>historyAddingDaysrepository,IEFGenericRepository<Weekend> wekendRepository,IEFGenericRepository<Vacation> vacationRepository)
        {
            Vacationrepository = vacationRepository;
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
        public IActionResult ShowVacationAll()
        {
            ViewBag.Team = TeamRepository.Get().ToList();
            return View();
        }
        public JsonResult GetEvents()
        {
            int vacationsCount = Vacationrepository.Get().Count();
            int i = 1;
            int numColors = 10;
            int k = 0;
            var colors = new List<string>();
            int integerRedValue = 8;
            //Green Value
            int integerGreenValue = 95;
            //Blue Value
            int integerBlueValue = 52;
            for (int j = 0; j < numColors; j++)
            {
                //var random = new Random(); // Don't put this here!


                string hexValue = '#' + integerRedValue.ToString("X2") + integerGreenValue.ToString("X2") + integerBlueValue.ToString("X2");

                colors.Add(hexValue);

                integerBlueValue += 24;
                integerGreenValue += 14;
                integerRedValue += 41;
                if (integerBlueValue > 255)
                    integerBlueValue = 0;
                if (integerGreenValue > 255)
                    integerGreenValue = 0;
                if (integerRedValue > 255)
                    integerRedValue = 0;
            }
            var events = new List<CalendarEventy>();
            List<Vacation> vacations = new List<Vacation>();
            List<Person> people = PersonRepository.IncludeGet(p=>p.Team).ToList();
            foreach (Person person in people)
            {
                vacations = Vacationrepository.Get(p => p.Peopleid == person.Id).ToList();

                DateTime start;
                DateTime end;
                var viewModel = new CalendarEventy();




                foreach (Vacation vacation in vacations)
                {
                    start = vacation.FirstDate;
                    end = vacation.SecontDate;
                    events.Add(new CalendarEventy()
                    {
                        Id = vacation.Id,
                        allDay = true,
                        title = "Vacation" + " " + person.LastName + " " + person.Name+" "+person.Team.TeamName,
                        start = start.ToString("yyyy-MM-dd"),
                        end = end.ToString("yyyy-MM-dd"),
                        backgroundColor = colors[i]
                    });


                }
                i++;
            }
            return Json(events.ToArray());
        }
        public JsonResult GetWeekend()
        {
            List<Weekend> Weekends = WeekendRepository.Get().ToList();
            List<DateForVacationAll> dates = new List<DateForVacationAll>();
            foreach (Weekend weekend in Weekends)
            {
                for (DateTime date = weekend.startDate; date < weekend.EndDate;)
                {
                    dates.Add(new DateForVacationAll
                    {
                        Id = Guid.NewGuid(),
                        Date = date.ToString()
                    });
                    date=date.AddDays(1);
                }
            }
            return Json(dates.ToArray());
        }
        public void ChangeDateVacation([FromBody] CalendarEventy request)
        {
         
            CountVacation countVacation = new CountVacation();
            string start = request.start.Substring(0 , 10).Replace("-","/");
            string end = request.end.Substring(0, 10).Replace("-", "/");
            Vacation updatevacation = Vacationrepository.FindById((Guid)request.Id);
            updatevacation.FirstDate = DateTime.ParseExact(start, "yyyy/M/d", CultureInfo.InvariantCulture);
            updatevacation.SecontDate = DateTime.ParseExact(end, "yyyy/M/d", CultureInfo.InvariantCulture);
           
            Vacationrepository.Update(updatevacation);
         
        }
    }
}
