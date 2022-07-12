using System;

namespace ProjectZero{

    

    class Menu{



        public static char printMenu(){

            Console.Clear();

            Console.WriteLine("Hello, welcome to your console newsletter.");
            Console.WriteLine("Please select from the following options.");
            Console.WriteLine("1. Top US Headlines.");
            Console.WriteLine("2. Search Top Headlines by country.");
            Console.WriteLine("3. Search by keyword.");
            Console.WriteLine("Press X to exit the program.");

            char choice = Generic.getChar();
            
            return choice;
        }//end printMenu


    } //close Menu

}//close namespace