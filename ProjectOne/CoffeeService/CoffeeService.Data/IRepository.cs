using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeService.Model
{
    public interface IRepository
    {
        Task<IEnumerable<Drink>> GetDrinksAsync();
        Task CreateDrinkAsync(Drink newDrink);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetSingleCustAsync(string name);
        Task<IEnumerable<AppOrder>> GetCustOrders(string name);
        Task CreateOrderAsync(OrderWrapper order);
        Task CreateCustomerAsync(string newCustomer);

    }
}
