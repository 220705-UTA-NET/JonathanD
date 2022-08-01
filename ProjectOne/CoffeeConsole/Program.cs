using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace CoffeeConsole
{
    public class Program
    {
        static readonly HttpClient client = new();
        

        static async Task Main()
        {
            StringBuilder uriBase = new StringBuilder("https://coffeeservice.azurewebsites.net/api/");
           
            Customer customer = new();
            Boolean loggedIn = false;
            int uSelection;
            string userName = " ";
            

            string custResponse = await client.GetStringAsync(uriBase.ToString() + "/Drinks/api/customers"); 
            List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(custResponse);

            List<string> customerNames = new();
            
            foreach(Customer x in customers)
            {
                customerNames.Add(x.userName);
            }

            //Logging in, creating new user if needed
            do{
                
                //Console.Clear(); 
                Console.WriteLine("Hello, welcome to CoffeeService!");
                Console.WriteLine("\n\n Are you a new or returning user?");
                Console.WriteLine("[1] Returning User");
                Console.WriteLine("[2] New User");
                uSelection = Convert.ToInt32(Console.ReadLine());

                switch(uSelection)
                {
                    case 1: //User sellects a returning user
                        Console.Clear();
                        Console.WriteLine("Please enter your username: ");
                        userName = Console.ReadLine();
                        
                        foreach(Customer x in customers)
                        {
                            if(x.userName == userName)
                            {
                                customer = x;
                                loggedIn = true;
                            }

                        }
                        
                        if(!loggedIn)
                        {
                            Console.WriteLine("User not found, please check spelling or enter a new user!");
                            Console.WriteLine("Press enter to continue...");
                            Console.Read();
                        }

                        Console.WriteLine("Logged in as: " + customer.userName);
                        Console.Read();

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine(uriBase);
                        Console.WriteLine("Please enter a username: ");
                        userName = Console.ReadLine();

                        if(!customerNames.Contains(userName))
                        {
                            string uri = uriBase + "Drinks/api/addCustomer";
                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri + $"/{userName}");


                            await client.PostAsync(uri + $"/{userName}", null);

                            string? response = await client.GetStringAsync(uriBase + $"Drinks/api/customer/{userName}");

                            customer = JsonSerializer.Deserialize<Customer>(response);
                            loggedIn = true;
                        }
                        else
                        {
                            Console.WriteLine("Username taken, please try again!");
                            Console.Read();
                        }
                        
                        Console.WriteLine("Logged in as: " + customer.userName);
                        Console.Read();
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again!");
                        Console.Read();
                        break;
                }

            } while(!loggedIn);

            
            //Main menu loop
            Console.Clear();
            uSelection = 0;
            bool exit = false;
            do
            {   
                Console.Clear();
                Console.WriteLine($"Welcome {customer.userName}!");
                Console.WriteLine("Please select from the following options:");
                Console.WriteLine("[1] New order");
                Console.WriteLine("[2] View order history");
                Console.WriteLine("[3] Exit CoffeeService");
                Console.Write("Enter selection: ");
                int.TryParse(Console.ReadLine(), out uSelection);

                

                switch(uSelection)
                {
                    case 1://create order

                        break;
                    case 2://view past orders
                        Console.Clear();
                        string uri = uriBase + $"Drinks/api/customer/{customer.userName}/orders";
                        string response = await client.GetStringAsync(uri);

                        List<AppOrder> orders = JsonSerializer.Deserialize<List<AppOrder>>(response);
                        
                        if(orders.Count == 0)
                        {
                            Console.WriteLine("You haven't placed any orders!");
                            Console.ReadLine();
                        }
                        else
                        {
                            foreach(AppOrder order in orders)
                            {
                                Console.WriteLine(order.ToString());
                            }
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadLine();

                        break;
                    case 3://exiting case
                        Console.Clear();
                        Console.WriteLine("Have a good day!");
                        Console.ReadLine();
                        exit = true;
                        break;
                    default:
                        break;
                }


            }while(!exit);

            return; // exits program
        }
    }
}