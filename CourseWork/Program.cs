using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            double mu = 1;
            double lambda = 0.5;
            Random random = new Random();

            double averageResponseTime = 0;
            int countOfDemands = 0;

            Queue<Demand> demands = new Queue<Demand>();
            double currentTime = 0;
            double ArrivalTime = -(1 / lambda) * Math.Log(random.NextDouble());
            double ServiceStartTime = Double.PositiveInfinity;
            double ServiceCompletionTime = Double.PositiveInfinity;
            double minTime = 0;

            Console.WriteLine("Ввод времени работы обслуживающего устройства: ");
            double MaxTime = Double.Parse(Console.ReadLine());

            while (currentTime < MaxTime)
            {
                minTime = Math.Min(Math.Min(ArrivalTime, ServiceStartTime), ServiceCompletionTime);

                if (minTime == ArrivalTime)
                {
                    ReceiptRequirements();
                    continue;
                }

                if (minTime == ServiceStartTime)
                {
                    ServiceStart();
                    continue;
                }

                if (minTime == ServiceCompletionTime)
                {
                    CareRequiremenrts();
                    continue;
                }

            }

            averageResponseTime /= countOfDemands;
            Console.WriteLine("Average response time = {0:f4}", averageResponseTime);




            void ReceiptRequirements()
            {
                Console.WriteLine("Поступление требования");
                Demand d = new Demand(ArrivalTime, ServiceStartTime, ServiceCompletionTime);
                if (demands.Count() == 0)
                {
                    ServiceStartTime = minTime;
                }
                demands.Enqueue(d);
                ArrivalTime = ArrivalTime + (-(1 / lambda) * Math.Log(random.NextDouble()));
            }

            void ServiceStart()
            {
                Console.WriteLine("Начало обслуживания");
                double ServiceTime = (-(1 / mu) * Math.Log(random.NextDouble())); 
                ServiceCompletionTime = ServiceTime + minTime;
                demands.Peek().ServiceStartTime = minTime; 
                ServiceStartTime = Double.PositiveInfinity;
            }

            void CareRequiremenrts()
            {
                Console.WriteLine("Уход требования");
                Demand d = demands.Dequeue(); 
                d.ServiceCompletionTime = minTime;
                if (demands.Count() > 0)
                {
                    ServiceStartTime = minTime;
                }
                ServiceCompletionTime = Double.PositiveInfinity;

                averageResponseTime += currentTime - d.ArrivingTime;
                countOfDemands++;
            }
        }
    }
}
