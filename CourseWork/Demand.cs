using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    public class Demand
    {
        public double ArrivingTime { get; set; }
        public double ServiceStartTime { get; set; }
        public double ServiceCompletionTime { get; set; }


        public Demand(double ArrivingTime, double ServiceStartTime, double ServiceCompletionTime)
        {
            this.ArrivingTime = ArrivingTime;
            this.ServiceStartTime = ServiceStartTime;
            this.ServiceCompletionTime = ServiceCompletionTime;
        }

        public double ResponseTime()
        {
            return ServiceCompletionTime - ArrivingTime;
        }
    }
}