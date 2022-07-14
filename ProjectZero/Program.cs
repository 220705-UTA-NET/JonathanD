
using System.Web;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using System;


namespace ProjectZero{

    class Program{

        static async Task Main(string[] args) //To use Async methods main MUST be asnyc and return a Task
        {   
            DotNetEnv.Env.Load(); //Loading env file
            var key = Environment.GetEnvironmentVariable("SECRET_KEY");//loading key

            StringBuilder sb = new StringBuilder("https://newsapi.org/v2/"); //base for all my URIs 
            
            var client = new HttpClient(); //creating HttpClient
            //adding User-Agent to header for all requests, API doesn't allow anonymous requests
            client.DefaultRequestHeaders.Add("User-Agent", "ProjectZero 0.08");

            char choice = ' '; //storing user input
            Boolean keepRunning = true;// value used to exit do-while

            do{ //do-while to print menu
                
                //Console.Clear();

                Console.WriteLine("Hello, welcome to your console newsletter.");
                Console.WriteLine("Please select from the following options.");
                Console.WriteLine("1. Top US Headlines.");
                Console.WriteLine("2. Search Top Headlines by country.");
                Console.WriteLine("3. Search by keyword.");
                Console.WriteLine("Press X to exit the program.");

                choice = Generic.getChar();
                choice = char.ToUpper(choice);
                //choice = Menu.printMenu();

                switch(choice){

                    case '1': // GET top US Headlines
                        var response = new HttpResponseMessage();
                        
                        sb.Append($"top-headlines?country=us&apiKey={key}");//appending sb to hit correct endpoint for this request
                        var uri = new Uri(sb.ToString());

                        response = await client.GetAsync(uri);

                        List<string> headlines = new List<string>{};
                        headlines.Add(await response.Content.ReadAsStringAsync());

                        foreach (string headline in headlines){
                            Console.WriteLine(headline);
                        }

                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case 'X':
                        keepRunning = false;//exits program if X is chosen
                        break;
                    default:

                        break;

                }//end switch
            } while (keepRunning);



            return;

        }//end Main

    }// end Program

}// end namepsace