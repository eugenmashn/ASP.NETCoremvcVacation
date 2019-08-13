using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Workers.ModelsView;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Workers.Models_View;
using System.Drawing;
using DataAccessLayer;
namespace Workers.Controllers
{
    [Authorize]
    [Route("[controller]/[action]/{personId?}")]
    public class VacationController : Controller
    {
        private static readonly Random rand = new Random();
        public IEFGenericRepository<Team> Teamrepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }
        public VacationController(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Weekend> weekendpository, IEFGenericRepository<Vacation> vacationrepository)
        {
            Personrepository = personrepository;
            Teamrepository = teamrepository;
            Weekendrepository = weekendpository;
            Vacationrepository = vacationrepository;
        }
        /*      [Route("/ShowVacation/{personId}") ]*/
        public IActionResult ShowVacation(Guid personId)
        {
            ViewBag.PersonId = personId;
            ViewBag.person = Personrepository.FindById(personId);
            List<Vacation> vacations = Vacationrepository.IncludeGet(t => t.People).Where(i => i.Peopleid == personId).ToList();
            return View(vacations);
        }
        /*      [Route("/AddnewVacation/{personId}")]*/
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddnewVacation(Guid personId)
        {
            List<Weekend> weekends = Weekendrepository.Get().ToList();
            Person person = Personrepository.FindById(personId);
            ViewBag.AddDays = person.Days;
            ViewBag.weekends = weekends;
            //ViewBag.AddDay=
            ViewBag.PersonId = personId;
            return View();
        }
        /*        [Route("/CreateNewVacation/{personId}")]*/
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddnewVacation(Guid personId,VacationView vacation)
        {
            // Person person = Personrepository.FindById(personId);
            Person person = Personrepository.IncludeGet(p => p.Team).FirstOrDefault(x => x.Id == personId);
            if (person == null)
                return View();
            CountVacation countVacation = new CountVacation();
            Vacation NewVacation = new Vacation();
            if (NewVacation.FirstDate>NewVacation.SecontDate|| countVacation.CountDaysVacation(NewVacation.FirstDate, NewVacation.SecontDate)>person.Days||NewVacation.SecontDate==null||NewVacation.FirstDate==null)
                   return Redirect("/Home/Workers/Home/Workers");
            NewVacation.Id = Guid.NewGuid();
            NewVacation.FirstDate = DateTime.ParseExact(vacation.startDay, "M/d/yyyy", CultureInfo.InvariantCulture); 
            NewVacation.SecontDate = DateTime.ParseExact(vacation.EndDay, "M/d/yyyy", CultureInfo.InvariantCulture);
       
            NewVacation.Days = countVacation.CountDaysVacation(NewVacation.FirstDate, NewVacation.SecontDate);
            NewVacation.People = person;
            CountVacation countvacation = new CountVacation();
            if (!countvacation.CheckonBusy(person,NewVacation.FirstDate,NewVacation.SecontDate)) { 
                ModelState.AddModelError("EndDay", "Please change date");
                ViewBag.AddDays = person.Days;
                List<Weekend> weekends = Weekendrepository.Get().ToList();
                ViewBag.weekends = weekends;
                //ViewBag.AddDay=
                ViewBag.PersonId = personId;
                VacationView vacationView = new VacationView();
                vacationView.startDay = NewVacation.FirstDate.ToString("M/d/yyyy");
                vacation.EndDay = NewVacation.SecontDate.ToString("M/d/yyyy");
                return View(vacation);
            }
            /* if (person.Days<= NewVacation.Days)
                 return Redirect("/Workers"); */
           
            person.Days -= countVacation.CountDaysVacation(NewVacation.FirstDate,NewVacation.SecontDate);
     
            Vacationrepository.Create(NewVacation);
            return Redirect("/Home/Workers/Home/Workers");
        }
           [Route("/Delete/{vacationId}/{personId}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteVacation(Guid vacationId,Guid personId)
        {
            Vacation vacation = Vacationrepository.FindById(vacationId);
            Person person = Personrepository.FindById(personId);
            person.Days += vacation.Days;
            Vacationrepository.Remove(vacation);
            Personrepository.Update(person);
            return Redirect("/Home/Workers/Home/Workers");
        }

        public IActionResult ShowCalendar(Guid PersonId)
        {
        
            return View();
        }
        public JsonResult GetEvents(Guid PersonId)
        {
            List<Vacation> vacations = Vacationrepository.Get(p => p.Peopleid == PersonId).ToList();
            DateTime start;
            DateTime end;
            var viewModel = new CalendarEventy();
            var events = new List<CalendarEventy>();
            int numColors = 10;
            var colors = new List<string>();
            for (int j = 0; j < numColors; j++)
            {
                var random = new Random(); // Don't put this here!
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }
            int i = 1;
            foreach(Vacation vacation in vacations)
            {
             start = vacation.FirstDate;
             end =vacation.SecontDate;
                events.Add(new CalendarEventy()
                {
                    Id= Guid.NewGuid(),
                    title = "Vacation"+i,
                    start = start.ToString("yyyy-MM-dd"),
                    end = end.ToString("yyyy-MM-dd"),
                    allDay = false,
                    backgroundColor=colors[i]
                });
                i++;
              
            }


            return Json(events.ToArray());
        }
    }
}