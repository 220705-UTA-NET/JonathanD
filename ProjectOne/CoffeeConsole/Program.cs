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









        }
    }
}