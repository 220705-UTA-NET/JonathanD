using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeService.Model
{
    public class Drink
    {
        public int drinkId { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public double price { get; set; }

        public Drink() { }

        public Drink(int drinkId, string name, string details, double price)
        {
            this.drinkId = drinkId;
            this.name = name;
            this.details = details;
            this.price = price;
        }


    }
}
