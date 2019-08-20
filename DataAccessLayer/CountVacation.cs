﻿using System;
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
      
        public IEFGenericRepository<Vacation> Vacationrepository;
        public IEFGenericRepository<Team> TeamRepository { get; set; }

        public IEFGenericRepository<Person> Personrepository { get; set; }
        public IEFGenericRepository<Weekend> Weekendrepository { get; set; }

        public CountVacation(IEFGenericRepository<Team> teamrepository, IEFGenericRepository<Person> personrepository, IEFGenericRepository<HistoryAddingDays> historyAddingDaysrepository, IEFGenericRepository<Weekend> wekendRepository, IEFGenericRepository<Vacation> vacationRepository)
        {
         
            Vacationrepository = vacationRepository;
            TeamRepository = teamrepository;
            Personrepository = personrepository;
            Weekendrepository = wekendRepository;
        }
        
        public int CountWeekend(DateTime StartDay, DateTime EndDay, string TeamName)
        {
            Team team = TeamRepository.Get().FirstOrDefault(t => t.TeamName == TeamName);
            if (team == null)
                return 0;
            int Count = 0;
            List<Vacation> vacationsTeam = Vacationrepository.IncludeGet(p => p.People).Where(x => x.People.TeamId == team.Id).ToList();

            List<Vacation> listTwo = vacationsTeam.Where(i => (ChackWeekend(StartDay, EndDay, i.FirstDate, i.SecontDate, TeamName, team.TeamName))).ToList();
            if (vacationsTeam == null)
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
          
            List<Weekend> listweekend = Weekendrepository.Get().ToList();
            foreach (var i in listweekend)
            {
                if (((DateTime.Compare(date.Date, i.startDate.Date) >= 0) && DateTime.Compare(date.Date, i.EndDate.Date) <= 0) || (date == i.startDate))
                {
                    return true;
                }
            }
            return false;
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
  
            if (CountTeam(person.Team.TeamName) - CountWeekend(FirstDate, SecondDate, person.Team.TeamName) <=person.Team.MinNumberWorkers )
            {
            
                return false;
            }
            return true;
        }
        private int CountTeam(string TeamName)
        {
           
            List<Person> list = Personrepository.IncludeGet(p=>p.Team).Where(i => i.Team.TeamName == TeamName).ToList();
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
