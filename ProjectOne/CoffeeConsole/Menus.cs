using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Text.Json;


namespace CoffeeConsole
{
    public class Menus
    {


        //USER SELECTION
        public async Task<Customer> UserSelectMenu(HttpClient client, StringBuilder uriBase)
        {   
            Customer customer = new();
            Boolean loggedIn = false;
            string? userName;
            int uSelection;
            

            string custResponse = await client.GetStringAsync(uriBase.Append("customers").ToString()); 
            List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(custResponse);
            

            do{
                
                Console.Clear(); 
                Console.WriteLine("Hello, welcome to CoffeeService!");
                Console.WriteLine("\n\n Are you a new or returning user?");
                Console.WriteLine("[1] Returning User");
                Console.WriteLine("[2] New User");
                uSelection = Console.Read();

                switch(uSelection)
                {
                    case 1: //User selects a returning user
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

                        break;
                    case 2:
                        
                        

                        break;
                    default:
                        break;
                }


                
            } while(!loggedIn);




            return customer;

        }


    
    }




}