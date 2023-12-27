using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var vacationDictionary = new Dictionary<string, List<DateTime>>();
            var workers = new List<string>()
            {
                "Иванов Иван Иванович",
                "Петров Петр Петрович",
                "Юлина Юлия Юлиановна",
                "Сидоров Сидор Сидорович",
                "Павлов Павел Павлович",
                "Георгиев Георг Георгиевич"
            };

            foreach (var worker in workers)
                vacationDictionary[worker] = VacationGenerator.GetVacationList();

            foreach (var vacationList in vacationDictionary)
                Console.WriteLine($"Дни отпуска {vacationList.Key} :\n{string.Join("\n", vacationList.Value.Select(date => date.ToShortDateString()))}\n");

            Console.ReadKey();
        }
    }
}
