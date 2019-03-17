using System;
// библиотека содержит "весь" функционал
using CourseWorkLibrary;

namespace DraftCourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            double lambda = 0.5;

            Random random = new Random();

            RequirementProcesses processes = new RequirementProcesses();

            processes.ArrivalTime = -(1 / lambda) * Math.Log(random.NextDouble());
            processes.ServiceStartTime = Double.PositiveInfinity;
            processes.ServiceCompletionTime = Double.PositiveInfinity;
            processes.CurrentTime = 0;

            Console.Write("Ввод времени работы обслуживающего устройства: ");
            double MaxTime = Double.Parse(Console.ReadLine());

            while (processes.CurrentTime < MaxTime)
            {
                processes.CurrentTime = Math.Min(Math.Min(processes.ArrivalTime,
                    processes.ServiceStartTime), processes.ServiceCompletionTime);

                if (processes.CurrentTime == processes.ArrivalTime)
                {
                    processes.ReceiptRequirements();
                    continue;
                }

                if (processes.CurrentTime == processes.ServiceStartTime)
                {
                    processes.ServiceStart();
                    continue;
                }

                if (processes.CurrentTime == processes.ServiceCompletionTime)
                {
                    processes.CareRequiremenrts();
                    continue;
                }
            }

            processes.AverageResponseTime /= processes.CountOfDemands;
            Console.WriteLine("Average response time = {0:f4}", processes.AverageResponseTime);
        }
    }
}
