using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class Continent
    {
        public Continent()
        {
        }
        public Continent(string name)
        {
            SetName(name);
        }
        public void SetPopulation(int pop) 
        {
            if(pop <0 )
                throw new Exception("Population is lower than 0");
            this.Population = pop;
        }
        public void SetName(String name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is null");
            this.Name = name;
        }
        public int ID { get; private set; }
        public String Name { get; private set; }
        public int Population { get; private set; }
    }
}
