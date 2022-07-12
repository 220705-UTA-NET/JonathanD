
using System;


namespace ProjectZero{

    class Program{

        static void Main(string[] args)
        {   
            Console.WriteLine("Running...");

            char choice = ' '; //storing user input
            Boolean exitChosen = false;

            //while (!exitChosen){
                
                Console.Clear();

                Console.WriteLine("Hello, welcome to your console newsletter.");
                Console.WriteLine("Please select from the following options.");
                Console.WriteLine("1. Top US Headlines.");
                Console.WriteLine("2. Search Top Headlines by country.");
                Console.WriteLine("3. Search by keyword.");
                Console.WriteLine("Press X to exit the program.");

                choice = Generic.getChar();

                //choice = Menu.printMenu();
                char.ToUpper(choice); //making choice uppercase always

                switch(choice){

                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case 'X':
                        exitChosen = true;//exits program if X is chosen
                        break;
                    default:

                        break;

                }//end switch
           // }



            return;

        }//end Main

    }// end Program

}// end namepsace