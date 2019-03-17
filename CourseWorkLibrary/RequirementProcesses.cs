using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseWorkLibrary
{
    public class RequirementProcesses
    {
        static double mu = 1.0;
        static double lambda = 0.5;

        Random random = new Random();

        // времена
        public double ArrivalTime { get; set; }
        public double ServiceStartTime { get; set; }
        public double ServiceCompletionTime { get; set; }
        public double CurrentTime { get; set; }

        Queue<Demand> demands = new Queue<Demand>();

        public int CountOfDemands { get; set; }
        public double AverageResponseTime { get; set; }

        public void ReceiptRequirements()
        {
            Console.WriteLine("Поступление требования");
            Demand d = new Demand(ArrivalTime, ServiceStartTime, ServiceCompletionTime);
            if (demands.Count() == 0)
            {
                ServiceStartTime = CurrentTime;
            }
            demands.Enqueue(d);
            ArrivalTime = ArrivalTime + (-(1 / lambda) * Math.Log(random.NextDouble()));
        }

        public void ServiceStart()
        {
            Console.WriteLine("Начало обслуживания");
            double ServiceTime = (-(1 / mu) * Math.Log(random.NextDouble()));
            ServiceCompletionTime = ServiceTime + CurrentTime;
            demands.Peek().ServiceStartTime = CurrentTime;
            ServiceStartTime = Double.PositiveInfinity;
        }

        public void CareRequiremenrts()
        {
            Console.WriteLine("Уход требования");
            Demand d = demands.Dequeue();
            d.ServiceCompletionTime = CurrentTime;
            if (demands.Count() > 0)
            {
                ServiceStartTime = CurrentTime;
            }
            ServiceCompletionTime = Double.PositiveInfinity;

            AverageResponseTime += CurrentTime - d.ArrivingTime;
            CountOfDemands++;
        }
    }
}
