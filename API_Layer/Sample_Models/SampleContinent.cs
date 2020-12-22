using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Sample_Models
{
    public class SampleContinent
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public List<String> Countries { get; set; } = new List<String>();
    }
}
