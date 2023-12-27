using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodTask
{
    public static class VacationGenerator
    {
        const int AllVacationsPause = 3;
        const int WorkerVacationsPause = 1;

        static readonly List<DateTime> vacations;
        static readonly Random gen;
        static readonly List<int> vacationSteps;

        static VacationGenerator()
        {
            vacations = new List<DateTime>();
            gen = new Random();
            vacationSteps = new List<int> { 7, 14 };
        }

        public static List<DateTime> GetVacationList()
        {
            var dateList = new List<DateTime>();

            var start = new DateTime(DateTime.Now.Year, 1, 1);
            var end = new DateTime(DateTime.Now.Year, 12, 31);
            var range = (end - start).Days;
            var vacationCount = 28;

            while (vacationCount > 0)
            {
                var startDate = start.AddDays(gen.Next(range));

                if (startDate.DayOfWeek == DayOfWeek.Sunday || startDate.DayOfWeek == DayOfWeek.Saturday)
                    continue;

                var endDate = GetEndDate(startDate, vacationCount == 7, out var difference);

                if (CanCreateVacation(dateList, startDate, endDate))
                {
                    for (var dt = startDate; dt < endDate; dt = dt.AddDays(1))
                    {
                        vacations.Add(dt);
                        dateList.Add(dt);
                    }
                    vacationCount -= difference;
                }
            }
            return dateList;
        }

        private static DateTime GetEndDate(DateTime startDate, bool oneWeek, out int difference)
        {
            var vacIndex = oneWeek ? 0 : gen.Next(vacationSteps.Count);
            difference = vacationSteps[vacIndex];
            return startDate.AddDays(difference);
        }

        private static bool CanCreateVacation(List<DateTime> dateList, DateTime startDate, DateTime endDate)
        {
            if (!vacations.Any(element => element.AddDays(AllVacationsPause) >= startDate && element.AddDays(-AllVacationsPause) <= endDate))
            {
                return !dateList.Any(element => element.AddMonths(WorkerVacationsPause) >= startDate
                    && element.AddMonths(-WorkerVacationsPause) <= endDate);
            }
            return false;
        }
    }
}
