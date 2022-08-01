using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingApi
{


    public class BerryResponse
    {
        public int count { get; set; }
        public string? next { get; set; }
        public object? previous { get; set; }
        public Berry[] results { get; set; }
    }

    public class Berry
    {
        public string name { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return this.name + " " + this.url;
        }

    }

}
