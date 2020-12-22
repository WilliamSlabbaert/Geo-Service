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

        public River(string name, double lenght, List<Country> con)
        {
            SetName(name);
            SetLenght(lenght);
            SetCountries(con);
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
        public void SetCountries(List<Country> con)
        {
            if (con == null || con.Count <=0)
                throw new Exception("No valid country list");
            this.Countries =con;
        }
        public void AddCountry(Country con)
        {
            if(con == null)
                throw new Exception("No valid country");
            if (!this.Countries.Contains(con))
                this.Countries.Add(con);
            else
                throw new Exception("Country already in list");
        }
        public int ID { get; private set; }
        public String Name { get; private set; }
        public Double Lenght { get; private set; }
        public virtual List<Country> Countries { get; private set; } = new List<Country>();
    }
}
