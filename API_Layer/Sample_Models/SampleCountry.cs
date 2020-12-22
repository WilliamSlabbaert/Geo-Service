using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Sample_Models
{
    public class SampleCountry
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public Double Surface { get; set; }
        public String Continent { get; set; }
        public List<String> Cities { get; set; } = new List<string>();
        public List<String> Rivers { get; set; } = new List<string>();

    }
}
