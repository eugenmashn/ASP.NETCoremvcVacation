using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Repository;
using DataAccessLayer.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer
{

    public class CountVacation 
    {
      
        public EFGenericRepository<Vacation> Vacationrepository;
        public EFGenericRepository<Team> TeamRepository { get; set; }

        public EFGenericRepository<Person> Personrepository { get; set; }
        public EFGenericRepository<Weekend> Weekendrepository { get; set; }

        public CountVacation()
        {
         var optionsBuilder = new DbContextOptionsBuilder<WorkerContext>();

         var options = optionsBuilder
                .UseSqlServer(@"Server=(localdb)\MsSqlLocalDb;Database=Workers;Trusted_Connection=True;")
                .Options;
            Vacationrepository = new EFGenericRepository<Vacation>(new WorkerContext(options));
            TeamRepository = new EFGenericRepository<Team>(new WorkerContext(options));
            Personrepository = new EFGenericRepository<Person>(new WorkerContext(options));
            Weekendrepository = new EFGenericRepository<Weekend>(new WorkerContext(options));
        }
        
        public int CountWeekend(DateTime StartDay, DateTime EndDay, string TeamName)
        {
            int Count = 0;
            List<Vacation> list = Vacationrepository.Get(i => i.TeamName == TeamName).ToList(); ;
            List<Vacation> listTwo = list.Where(i => (ChackWeekend(StartDay, EndDay, i.FirstDate, i.SecontDate, TeamName, i.TeamName))).ToList();
            if (list == null)
                return 0;
            Count = listTwo.Count();
            return Count;
        }
        public int CountDays(DateTime CountDate, int Days)
        {
            int IndexDay = Days;
            for (int i = 0; i <= IndexDay; i++)
            {
                if (CountDate.DayOfWeek == DayOfWeek.Sunday || CountDate.DayOfWeek == DayOfWeek.Saturday || AuditDate(CountDate))
                {
                    IndexDay++;
                }
                CountDate = CountDate.AddDays(1);
            }
            return IndexDay;
        }
        public bool ChackWeekend(DateTime StartDay, DateTime EndDay, DateTime StartDaySecond, DateTime EndDaySecond, string TeamNameOne, string TeamNameTwo)
        {
            if (TeamNameOne != TeamNameTwo)
                return false;
            bool check = false;
            for (DateTime indexFirstDate = StartDay; indexFirstDate <= EndDay;)
            {
                for (DateTime indexSecondDate = StartDaySecond; indexSecondDate <= EndDaySecond;)
                {
                    if (indexFirstDate.Date == indexSecondDate.Date)
                    {
                        return true;

                    }

                    indexSecondDate = indexSecondDate.AddDays(1);
                }

                indexFirstDate = indexFirstDate.AddDays(1);
            }

            return check;
        }
        public bool AuditDate(DateTime date)
        {
            bool TrueorFalse = false;
            List<Weekend> listweekend = Weekendrepository.Get().ToList();
            foreach (var i in listweekend)
            {
                if (((DateTime.Compare(date.Date, i.startDate.Date) >= 0) && DateTime.Compare(date.Date, i.EndDate.Date) <= 0) || (date == i.startDate))
                {
                    TrueorFalse = true;
                }
            }
            return TrueorFalse;
        }
        public int ResultCountAddDay(DateTime CountDate, Person person)
        {
           
            List<Person> IndexDayTwo = Personrepository.Get().ToList();
              int IndexDay = person.Days;


                for (int i = 0; i <= IndexDay; i++)
                {
                    if (CountDate.DayOfWeek == DayOfWeek.Sunday || CountDate.DayOfWeek == DayOfWeek.Saturday || AuditDate(CountDate))
                    {
                        IndexDay++;
                    }
                    CountDate = CountDate.AddDays(1);
                }
              
            
            return IndexDay;
        }
        public bool CheckonBusy(Person person,DateTime FirstDate, DateTime SecondDate)
        {
            bool check = true;
            if (CountTeam(person.Team.TeamName) - CountWeekend(FirstDate, SecondDate, person.Team.TeamName) <=person.Team.MinNumberWorkers && person.Team.MinNumberWorkers != 0)
            {
            
                check =false;
            }
            return false;
        }
        private int CountTeam(string TeamName)
        {
            List<Person> list = Personrepository.Get(i => i.Team.TeamName == TeamName).ToList();
            return list.Count;
        }
        public int CountDaysVacation(DateTime startDate, DateTime FinishDate)
        {
            int CountDaysHolyDays = 0;
            int IndexDay = 0;
            for (DateTime i = startDate; i <= FinishDate;)
            {
                if (i.DayOfWeek == DayOfWeek.Sunday || i.DayOfWeek == DayOfWeek.Saturday || AuditDate(i))
                {
                    IndexDay++;
                    CountDaysHolyDays--;
                }
                CountDaysHolyDays++;
                i = i.AddDays(1);
            }
            return CountDaysHolyDays;
        }
    }
}
