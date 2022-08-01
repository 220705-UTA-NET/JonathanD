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
        // public static async Task<string> UserSelectMenu(HttpClient client, StringBuilder uriBase)
        // {   
        //     Customer customer = new();
        //     Boolean loggedIn = false;
        //     string? userName;
        //     int uSelection;
            

        //     string custResponse = await client.GetStringAsync(uriBase.Append("customers").ToString()); 
        //     List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(custResponse);

        //     List<string> customerNames = new();
            
        //     foreach(Customer x in customers)
        //     {
        //         customerNames.Add(x.userName);
        //     }

        //     do{
                
        //         Console.Clear(); 
        //         Console.WriteLine("Hello, welcome to CoffeeService!");
        //         Console.WriteLine("\n\n Are you a new or returning user?");
        //         Console.WriteLine("[1] Returning User");
        //         Console.WriteLine("[2] New User");
        //         uSelection = Console.Read();

        //         switch(uSelection)
        //         {
        //             case 1: //User sellects a returning user
        //                 Console.Clear();
        //                 Console.WriteLine("Please enter your username: ");
        //                 userName = Console.ReadLine();
                        
        //                 foreach(Customer x in customers)
        //                 {
        //                     if(x.userName == userName)
        //                     {
        //                         customer = x;
        //                         loggedIn = true;
        //                     }

        //                 }
                        
        //                 if(!loggedIn)
        //                 {
        //                     Console.WriteLine("User not found, please check spelling or enter a new user!");
        //                     Console.WriteLine("Press enter to continue...");
        //                     Console.Read();
        //                 }

        //                 Console.WriteLine("Logged in as: " + customer.userName);
        //                 Console.Read();

        //                 break;
        //             case 2:
        //                 Console.Clear();

        //                 Console.WriteLine("Please enter a username: ");
        //                 userName = Console.ReadLine();

        //                 if(!customerNames.Contains(userName))
        //                 {
        //                     var httpRequest = new HttpRequestMessage(HttpMethod.Post, uriBase + "addCustomer");
        //                     httpRequest.Content.Equals(userName);
        //                     await client.SendAsync(httpRequest);
                            
        //                     string response = await client.GetStringAsync(uriBase + $"customer/{userName}");

        //                     customer = JsonSerializer.Deserialize<Customer>(response);
        //                     loggedIn = true;
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("Username taken, please try again!");
        //                     Console.Read();
        //                 }
                        
        //                 Console.WriteLine("Logged in as: " + customer.userName);
        //                 Console.Read();
        //                 break;
        //             default:
        //                 Console.WriteLine("Invalid option, please try again!");
        //                 Console.Read();
        //                 break;
        //         }


                
        //     } while(!loggedIn);


        //     return JsonSerializer.Serialize(customer);

        // }
    }




}