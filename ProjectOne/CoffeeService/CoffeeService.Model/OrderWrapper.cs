using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeService.Model
{
    public class OrderWrapper
    {

        public Customer? Customer { get; set; }
        public List<Drink>? Drinks { get; set; }

        public OrderWrapper() { }

        public OrderWrapper(Customer customer, List<Drink> drinks)
        {
            this.Customer = customer;
            this.Drinks = drinks;
        }

    }
}
