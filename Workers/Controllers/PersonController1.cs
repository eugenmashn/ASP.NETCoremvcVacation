using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Workers.Controllers
{
    public class PersonController : Controller
    {

        public IEFGenericRepository<Team> TeamRepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }

        public PersonController(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamRepository)
        {
            Personrepository = personrepository;
            TeamRepository = teamRepository;

        }
    
        // GET: /<controller>/
        [HttpGet]
        [Route("/AddnewPerson/{TeamId}")]
        public IActionResult AddnewPerson(Guid TeamId) {
            ViewData["Message"] = "Add NewPerson  page.";
            return View();
        }
        [HttpPost]
        [Route("{TeamId}")]
        public IActionResult CreatenewPerson(Person person,Guid TeamId)
        {
            Person newPerson = new Person();
            newPerson.Id = Guid.NewGuid();
            newPerson.Year = DateTime.Now.Year;
            newPerson.Name = person.Name;
            newPerson.LastName = person.LastName;
            newPerson.TeamId = TeamId;
            newPerson.Days = person.Days;

            Personrepository.Create(newPerson);
            return Redirect("~/");
        }
        [HttpGet]
        [Route("ChangePerson/{personId}")]
        public IActionResult ChangePerson(Guid personId)
        {
            Person person = Personrepository.FindById(personId);

            return View(person);
        }
        [HttpPost]
        [Route("ChangePerson/{personId}")]
        public IActionResult ChangePersonPost(Person person,Guid personId)
        {
            Person Updateperson = Personrepository.FindById(personId);
            Updateperson.Name = person.Name;
            Updateperson.LastName = person.LastName;
            Updateperson.Days = person.Days;
            Personrepository.Update(Updateperson);
            return Redirect("~/Workers");
        }
    }
}
