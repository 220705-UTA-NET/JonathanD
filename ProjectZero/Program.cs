
using System;
using System.Text;
using System.Net;
using System.IO;
using System;


namespace ProjectZero{

    class Program{

        static void Main(string[] args)
        {   
            Console.WriteLine("Running...");

            char choice = ' '; //storing user input
            Boolean keepRunning = true;

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

                    case '1':

                        
                        using (var client = new HttpClient())
                        {
                            var endpoint = new Uri("https://newsapi.org/v2/everything?q=bitcoin&apiKey=6d4a7b4f281c43fbb67e55013a379776");
                            var result = client.GetAsync(endpoint).Result;
                            var json = result.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(json);
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