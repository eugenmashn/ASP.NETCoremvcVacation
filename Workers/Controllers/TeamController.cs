using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workers.Models;
using Workers.Models_View;

namespace Workers.Controllers
{
/*    [Authorize]*/
    public class TeamController : Controller
    {
        public IEFGenericRepository<Team> TeamRepository { get; set;}

        public IEFGenericRepository<Person> Personrepository { get; set; }

        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }
        public TeamController(IEFGenericRepository<Person> personrepository,IEFGenericRepository<Team> teamRepository,IEFGenericRepository<Vacation> vacationRepository)
        {
            Personrepository = personrepository;
            TeamRepository = teamRepository;
            Vacationrepository = vacationRepository;

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
        [Authorize(Roles = "admin")]
        public IActionResult TeamDelete(Guid TeamId)
        {
            Team team = TeamRepository.FindById(x=>x.Id==TeamId);
            TeamRepository.Remove(team);
             return Redirect("~/");
        }
        [Route("Team/ShowvacationTeam/{TeamId}")]
        public IActionResult ShowvacationTeam(Guid TeamId)
        {

            return View();
        }
        [Route("Team/GetEvents/{TeamId}")]
        public JsonResult GetEvents(Guid TeamId)
        {
            int i = 1;
            int numColors = 10;
            var colors = new List<string>();
            for (int j = 0; j < numColors; j++)
            {
                var random = new Random(); // Don't put this here!
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }
            var events = new List<CalendarEventy>();
            List<Vacation> vacations = new List<Vacation>();
            List<Person> people = Personrepository.Get(p => p.TeamId == TeamId).ToList();
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
                        Id = Guid.NewGuid(),
                        title = "Vacation" + person.LastName+person.Name,
                        start = start.ToString("yyyy-MM-dd"),
                        end = end.ToString("yyyy-MM-dd"),
                        allDay = false,
                        backgroundColor = colors[i]
                    });
                    

                }
                  i++;
            }
           return Json(events.ToArray());  
        }
    }
}
