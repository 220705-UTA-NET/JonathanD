using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeService.Model;
using Microsoft.AspNetCore.Cors;



namespace CoffeeService.App.Controllers
{

    //When enabling CORS we reference it by the string in the Controller. 
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {

        // Fields
        private readonly IRepository _repo;
        private readonly ILogger<DrinksController> _logger;

        // Constructor
        public DrinksController(IRepository repo, ILogger<DrinksController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Methods

        //GET REQUESTS

        //GET /api/drinks
        [HttpGet("api/drinks")]
        public async Task<ActionResult<IEnumerable<Drink>>> GetAllDrinks()
        {
            IEnumerable<Drink> drinks;

            try
            {
                drinks = await _repo.GetDrinksAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            
            

            return drinks.ToList();
        }

        //GET /api/customers
        [HttpGet("api/customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            IEnumerable<Customer> customers;

            try
            {
                customers = await _repo.GetCustomersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return customers.ToList();
        }

        //GET /api/singleCustomer
        [HttpGet("api/customer/{name}")]
        public async Task<ActionResult<Customer>> GetSingleCust(string name)
        {
            Customer customer;

            try
            {
                customer = await _repo.GetSingleCustAsync(name);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return customer;
        }

        //GET /api/order
        [HttpGet("api/customer/{name}/orders")]
        public async Task<ActionResult<IEnumerable<AppOrder>>> GetCustOrdersAsync([FromRoute] string name)
        {
            IEnumerable<AppOrder> orders;

            try
            {
                orders = await _repo.GetCustOrders(name);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return orders.ToList();
        }


        //POST REQUESTS

        //Post /api/drinks
        [HttpPost("api/addDrink")]
        public async Task<ActionResult<IEnumerable<Drink>>> PostNewDrink([FromBody] Drink newDrink)
        {

            try
            {
                await _repo.CreateDrinkAsync(newDrink);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("api/addOrder")]
        public async Task<ActionResult<OrderWrapper>> PostNewOrder([FromBody] OrderWrapper order)
        {
            try
            {
                await _repo.CreateOrderAsync(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("api/addCustomer/{customer}")]
        public async Task<ActionResult<Customer>> PostNewCustomer([FromRoute] string customer)
        {
            try
            {
                await _repo.CreateCustomerAsync(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok();
        }

    }
}
