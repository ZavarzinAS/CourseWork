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
            double lambda = 0.5;
            double mu = 1;

            double averageResponseTime = 0;
            int countOfDemands = 0; 
            
            //Названия классов с заглавной буквы, но не переменных (есть code style) 
            //методы и свойства с заглавной
            //можно сделать автоматически через rename или refactor в вижаке
            //https://docs.microsoft.com/en-us/visualstudio/ide/reference/rename?view=vs-2017
            Random random = new Random();

            Queue<Demand> demands = new Queue<Demand>();
            double currentTime = 0;
            double ArrivalTime = -(1 / lambda) * Math.Log(random.NextDouble());
            //Изначально мы про эти события ничего не знаем (здесь основная проблема!) 
            double ServiceStartTime = Double.PositiveInfinity;
            double ServiceCompletionTime = Double.PositiveInfinity;
            double minTime = 0;

            Console.WriteLine("Ввод времени работы обслуживающего устройства: ");
            double MaxTime = Double.Parse(Console.ReadLine());

            //А это напоминает бесконечный цикл
            while (currentTime < MaxTime)
            {
                minTime = Math.Min(Math.Min(ArrivalTime, ServiceStartTime), ServiceCompletionTime);
                Console.WriteLine("min time = {0:f4}", minTime);

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

            averageResponseTime/= countOfDemands; 
            Console.WriteLine("Average response time = {0:f4}", averageResponseTime);

            
            
            
            
            
            void ReceiptRequirements()
            {
                Console.WriteLine("Поступление требования");
                //Можно сделать несколько конструкторов т.к изначально известно только одно значение ArrivalTime
                Demand d = new Demand(ArrivalTime, ServiceStartTime, ServiceCompletionTime);
                if (demands.Count() == 0)
                {
                    ServiceStartTime = minTime;
                }

                demands.Enqueue(d);
                //Можно вынести в отдельный метод, которая будет возвращать псевдослучаное число 
                // по заданному параметру
                ArrivalTime = ArrivalTime + (-(1 / lambda) * Math.Log(random.NextDouble()));
            }

            void ServiceStart()
            {
                Console.WriteLine("Начало обслуживания");
                double ServiceTime = (-(1 / mu) * Math.Log(random.NextDouble()));
                ServiceCompletionTime = ServiceTime + minTime;
                //Это метод, а не свойство => ставим скобки
                demands.Peek().ServiceStartTime = minTime;
                //Это точно больше  любого значения
                ServiceStartTime = double.PositiveInfinity;
            }

            void CareRequiremenrts()
            {
                Console.WriteLine("Уход требования");
                //Это метод - ставим скобки
                Demand d = demands.Dequeue(); //Извлекаем требование из очереди
                d.ServiceCompletionTime = minTime;
                if (demands.Count() > 0)
                {
                    ServiceStartTime = minTime;
                }

                ServiceCompletionTime = double.PositiveInfinity;

                //Подсчитаем некоторую статистику - среднее для длительности пребывания
                //аналитика говорит, что должно быть 1/(mu -lambda)
                averageResponseTime += currentTime - d.ArrivingTime;
                countOfDemands++; 
            }
        }
    }
}