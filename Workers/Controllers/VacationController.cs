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
namespace Workers.Controllers
{
    public class VacationController : Controller
    {
        public IEFGenericRepository<Team> TeamRepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public IEFGenericRepository<Vacation> Vacationrepository { get; set; }
        public VacationController(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamRepository, IEFGenericRepository<Weekend> weekendpository, IEFGenericRepository<Vacation> vacationrepository)
        {
            Personrepository = personrepository;
            TeamRepository = teamRepository;
            Weekendrepository = weekendpository;
            Vacationrepository = vacationrepository;
        }
        [Route("/ShowVacation/{personId}") ]
        public IActionResult ShowVacation(Guid personId)
        {
            ViewBag.PersonId = personId;
            List<Vacation> vacations = Vacationrepository.IncludeGet(t=>t.People).Where(i => i.Peopleid == personId).ToList();
            return View(vacations);
        }
        [Route("/AddnewVacation/{personId}")]
        public IActionResult AddnewVacation(Guid personId)
        {
            ViewBag.PersonId = personId;
            return View();
        }
        [Route("/CreateNewVacation/{personId}")]
        public IActionResult CreateNewVacation(Guid personId,VacationTwo vacation)
        {
            Person person = Personrepository.FindById(personId);
            
            CountVacation countVacation = new CountVacation();
            Vacation NewVacation = new Vacation();
            NewVacation.Id = Guid.NewGuid();
            NewVacation.FirstDate = DateTime.ParseExact(vacation.startDay, "M/d/yyyy", CultureInfo.InvariantCulture); 
            NewVacation.SecontDate = DateTime.ParseExact(vacation.EndDay, "M/d/yyyy", CultureInfo.InvariantCulture);
            NewVacation.Days = countVacation.CountDaysVacation(NewVacation.FirstDate, NewVacation.SecontDate);
            NewVacation.People = person;
            if (person.Days< NewVacation.Days)
                return Redirect("/Workers"); ;
            person.Days -= NewVacation.Days;
            Vacationrepository.Create(NewVacation);
            return Redirect("/Workers");
        }
        [Route("/Delete/{vacationID}/{personId}")]
        public IActionResult DeleteVacation(Guid vacationID,Guid personId)
        {
            Vacation vacation = Vacationrepository.FindById(vacationID);
            Person person = Personrepository.FindById(personId);
            person.Days += vacation.Days;
            Vacationrepository.Remove(vacation);
            Personrepository.Update(person);
            return Redirect("/Workers");
        }
    }
}