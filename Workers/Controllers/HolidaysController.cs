using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Repository;
using DataAccessLayer.Models;
using Workers.Models_View;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Workers.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class HolidaysController : Controller
    {
        public IEFGenericRepository<Team> TeamRepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public HolidaysController(IEFGenericRepository<Person> personrepository, IEFGenericRepository<Team> teamRepository,IEFGenericRepository<Weekend>weekendpository)
        {
            Personrepository = personrepository;
            TeamRepository = teamRepository;
            Weekendrepository = weekendpository;
        }
       /* [Route("/HolydaysView")]*/
        public IActionResult HolydaysView()
        {
           List<Weekend> weekend = Weekendrepository.Get().ToList();
           return View(weekend);
        }
       /* [Route("/CreateHolydays")]*/
        public IActionResult CreateHolydays() {

            //  return Redirect("/HolydaysView");
            return View();
        }
        public partial class DateRangePickerController : Controller
        {

            public IActionResult Index()
            {
                return View();
            }
        }
   /*     [Route("/CreateNewHolydays")]*/
        public IActionResult CreateNewHolydays(Holydays newweekend)
        {
            Weekend weekend = new Weekend();
            weekend.Id = Guid.NewGuid();
            weekend.startDate = DateTime.ParseExact(newweekend.startDay, "M/d/yyyy", CultureInfo.InvariantCulture);
            weekend.EndDate = DateTime.ParseExact(newweekend.EndDay, "M/d/yyyy", CultureInfo.InvariantCulture);
            Weekendrepository.Create(weekend);
            return Redirect("/HolydaysView");
        }
      /*  [Route("/DeleteHolyDay/{holidaysId}")]*/
        public IActionResult DeleteHolyDay(Guid holidaysId)
        {
            Weekend weekend = Weekendrepository.FindById(holidaysId);
            Weekendrepository.Remove(weekend);
            return Redirect("/HolydaysView");
        }
    }
}