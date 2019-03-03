using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            double lambda = 0.5;
            Random R = new Random();

            Queue<Demand> demands = new Queue<Demand>();
            double Time = 0;
            double ArrivalTime = -(1 / lambda) * Math.Log(R.NextDouble());
            double ServiceStartTime = 0;
            double ServiceCompletionTime = 0;
            double minTime;

            Console.WriteLine("Ввод времени работы обслуживающего устройства: ");
            double MaxTime = Double.Parse(Console.ReadLine());

            while (Time < MaxTime)
            {

                if (ArrivalTime < ServiceStartTime && ArrivalTime < ServiceCompletionTime)
                {
                    minTime = ArrivalTime;
                }
                else
                {
                    if (ServiceStartTime < ServiceCompletionTime)
                    {
                        minTime = ServiceStartTime;
                    }
                    else
                        minTime = ServiceCompletionTime;
                }

                if (minTime == ArrivalTime)
                {
                    ReceiptRequirements();
                }

                if (minTime == ServiceStartTime)
                {
                    ServiceStart();
                }

                if (minTime == ServiceCompletionTime)
                {
                    CareRequiremenrts();
                }

            }

            void ReceiptRequirements()
            {
                Console.WriteLine("Поступление требования");
                Demand d = new Demand(ArrivalTime, ServiceStartTime, ServiceCompletionTime);
                if (demands.Count() == 0)
                    ServiceStartTime = minTime;
                demands.Enqueue(d);
                ArrivalTime = ArrivalTime + (-(1 / lambda) * Math.Log(R.NextDouble()));
            }

            void ServiceStart()
            {
                Console.WriteLine("Начало обслуживания");
                double ServiceTime = ?; //?
                ServiceCompletionTime = ServiceTime + minTime;
                demands.Peek.ServiceStartTime = minTime; //?
                ServiceStartTime = double.MaxValue;
            }

            void CareRequiremenrts()
            {
                Console.WriteLine("Уход требования");
                Demand d = demands.Take<>; //?
                d.ServiceCompletionTime = minTime;
                if (demands.Count() > 0)
                {
                    ServiceStartTime = minTime;
                }
                ServiceCompletionTime = double.MaxValue;
            }
        }
    }
}
