using Microsoft.Extensions.Logging;
using CoffeeService.Model;
using System.Data.SqlClient;

namespace CoffeeService.Data
{
    public class SQLRepository : IRepository 
    {   

        //Fields
        public readonly string _connectionString;

        private readonly ILogger<SQLRepository> _logger;

        //Constructor
        public SQLRepository(string connectionString, ILogger<SQLRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        //Methods
        public async Task<IEnumerable<Drink>> GetDrinksAsync()
        {
            List<Drink> drinks = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText = "SELECT drinkId, name, details, price FROM dbo.Drinks;";

            using SqlCommand command = new(commandText, connection);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while(await reader.ReadAsync()) 
            {
                int drinkId = reader.GetInt32(0);
                string name = reader.GetString(1);
                string details = reader.GetString(2);
                double price = reader.GetDouble(3);

                Drink tempDrink = new Drink(drinkId, name, details, price);
                drinks.Add(tempDrink);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetDrinksAsync, returned {0} results", drinks.Count);
           
            return drinks;
        }

        public async Task CreateDrinkAsync(Drink newDrink)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText;
            commandText = String.Format("INSERT INTO dbo.Drinks (name, details,price) VALUES('{0}', '{1}', {2});", newDrink.name, newDrink.details, newDrink.price);
            using SqlCommand command = new(commandText, connection);

            try { await command.ExecuteNonQueryAsync(); } catch (Exception e) { _logger.LogError(e, e.Message); };

            _logger.LogInformation("Added a drink to the DB");

            return;

        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText = "SELECT customerId, userName FROM dbo.Customers;";

            using SqlCommand command = new(commandText, connection);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int customerId = reader.GetInt32(0);
                string userName = reader.GetString(1);
                

                Customer tempCust = new Customer(customerId, userName);
                customers.Add(tempCust);
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetCustomersAsync, returned {0} results", customers.Count);

            return customers;
        }

        public async Task<Customer> GetSingleCustAsync(string name)
        {
            Customer customer = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText;
            commandText = string.Format("SELECT customerId, userName FROM dbo.Customers WHERE userName='{0}';", name);

            using SqlCommand command = new(commandText, connection);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int customerId = reader.GetInt32(0);
                string userName = reader.GetString(1);


                Customer tempCust = new Customer(customerId, userName);
                customer = tempCust;
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetSingleCustAsync");

            return customer;
        }

        public async Task<IEnumerable<AppOrder>> GetCustOrders(string custName)
        {
            List<AppOrder> orders = new();
            
            List<int> custId = new();
            List<int> orderIds = new();

            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText = string.Format(@"SELECT Customers.customerId FROM dbo.Customers 
                WHERE Customers.userName='{0}';", custName);

            //Getting customer Id
            using SqlCommand command1 = new(commandText, connection);
            using SqlDataReader reader = await command1.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                int tmpCustId = reader.GetInt32(0);
                custId.Add(tmpCustId);
            }

            //Getting orders
            commandText = string.Format("SELECT orderId FROM Orders WHERE customerId={0};", custId[0]);

            using SqlCommand command2 = new(commandText, connection);
            using SqlDataReader reader2 = await command2.ExecuteReaderAsync();

            while (await reader2.ReadAsync())
            {
                int orderId = reader2.GetInt32(0);
                orderIds.Add(orderId);
            }

            _logger.LogInformation(orderIds.ToString());

            //Getting drinks per order, creating full AppOrders
            foreach(int orderId in orderIds)
            {
                List<int> drinkIds = new();
                List<Drink> drinks = new();
                commandText = string.Format("SELECT drinkId FROM OrderDrinks WHERE orderId={0};", orderId);

                using SqlCommand command3 = new(commandText, connection);
                using SqlDataReader reader3 = await command3.ExecuteReaderAsync();

                while(await reader3.ReadAsync())
                {
                    int drinkId = reader3.GetInt32(0);
                    _logger.LogInformation(drinkId.ToString());
                    drinkIds.Add(drinkId);
                }

                //gets list of drinks per order
                foreach(int id in drinkIds)
                {
                    _logger.LogInformation(id.ToString());
                    string cmdTxt = string.Format(@"SELECT drinkId, name, details, price " +  
                         @" FROM dbo.Drinks WHERE drinkId={0};", id);

                    _logger.LogInformation(cmdTxt);

                    using SqlConnection connection2 = new(_connectionString);
                    await connection2.OpenAsync();

                    using SqlCommand command4 = new(cmdTxt, connection2);
                    using SqlDataReader reader4 = command4.ExecuteReader();

                    while(reader4.Read()) 
                    {
                        int drinkId = reader4.GetInt32(0);
                        string name = reader4.GetString(1);
                        string details = reader4.GetString(2);
                        double price = reader4.GetDouble(3);

                        Drink tempDrink = new Drink(drinkId, name, details, price);
                        drinks.Add(tempDrink);
                    }
                    command4.Parameters.Clear();
                    await connection2.CloseAsync();
                }
                orders.Add(new AppOrder(custName, drinks, orderId));
            }

            await connection.CloseAsync();

            _logger.LogInformation("Executed GetCustOrders, returned {0} results", orders.Count);

            return orders;
        }

        public async Task CreateOrderAsync(OrderWrapper order)
        {
            using SqlConnection connection1 = new(_connectionString);
            await connection1.OpenAsync();
            string commandText1;
            commandText1 = String.Format(@"INSERT INTO dbo.Orders(customerId) VALUES ({0});", order.Customer.customerId);
            using SqlCommand command1 = new(commandText1, connection1);

            try { await command1.ExecuteNonQueryAsync(); } catch (Exception e) { _logger.LogError(e, e.Message); }

            await connection1.CloseAsync();

            //Getting that same orderId
            List<int> orderId = new();
            using SqlConnection connection2 = new(_connectionString);
            await connection2.OpenAsync();
            string commandText2;
            commandText2 = String.Format(@"SELECT orderId FROM dbo.Orders WHERE orderId = (SELECT MAX(orderId) FROM dbo.Orders);");
            using SqlCommand command2 = new(commandText2, connection2);

            using SqlDataReader read = await command2.ExecuteReaderAsync();

            while(await read.ReadAsync())
            {
                int tmp = read.GetInt32(0);
                _logger.LogInformation(tmp.ToString());
                orderId.Add(tmp);
            }

            await connection2.CloseAsync();

            if (orderId.Count > 0)
            {
                //Creating OrderDrinks rows
                foreach (Drink drink in order.Drinks)
                {
                    using SqlConnection connection3 = new(_connectionString);
                    await connection3.OpenAsync();
                    string commandText3;
                    commandText3 = String.Format(@"INSERT INTO dbo.OrderDrinks(orderId,drinkId) VALUES ({0},{1});", orderId[0], drink.drinkId);
                    using SqlCommand command3 = new(commandText3, connection3);

                    try { await command3.ExecuteNonQueryAsync(); } catch (Exception e) { _logger.LogError(e, e.Message); }

                    command3.Parameters.Clear();
                    await connection3.CloseAsync();
                }
            }

        }

        public async Task CreateCustomerAsync(string newCustomer)
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string commandText;
            commandText = String.Format(@"INSERT INTO dbo.Customers (userName) VALUES ('{0}');", newCustomer);
            using SqlCommand command = new(commandText, connection);

            try { await command.ExecuteNonQueryAsync(); } catch (Exception e) { _logger.LogError(e, e.Message); };

            _logger.LogInformation("Added " + newCustomer + " as a customer to the DB!");

            return;

        }

    }
}