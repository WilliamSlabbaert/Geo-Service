using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer.Sample_Models
{
    public class SampleRiver
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public Double Lenght { get; set; }
        public List<String> Countries { get; set; } = new List<string>();
    }
}
