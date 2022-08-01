using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeService.Model
{
    public class AppOrder
    {
        public string userName { get; set; }
        public int orderId { get; set; }
        public List<Drink> drinks { get; set; }

        public AppOrder() { }

        public AppOrder(string userName, List<Drink> drinks, int orderId)
        {
            this.userName = userName;
            this.drinks = drinks;
            this.orderId = orderId;
        }
    }
}
