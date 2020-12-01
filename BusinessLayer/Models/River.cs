using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class River
    {
        public River()
        {
        }

        public River(string name, double lenght)
        {
            Name = name;
            Lenght = lenght;
        }
        public void SetName(String name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is null");
            this.Name = name;
        }
        public void SetLenght(Double length)
        {
            if (length == null||length < 0)
                throw new Exception("Length is lower than 0");
            this.Lenght = length;
        }
        public int ID { get; set; }
        public String Name { get; set; }
        public Double Lenght { get; set; }
        public List<Country> Countries { get; set; } = new List<Country>();
    }
}
