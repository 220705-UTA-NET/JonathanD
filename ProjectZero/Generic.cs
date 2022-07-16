using System;

namespace ProjectZero{

    class Generic{

        public static char getChar(){ //getting a single char value from console
            char input = Console.ReadKey().KeyChar; //Reads user input from keyboard
            char.ToUpper(input);
            return input;
        }//end getInpit

        public static string? getString(string input){

            input = Console.ReadLine();

            if(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)){
                Console.WriteLine("Don't submit empty strings, try again.");
                getString(input);
            } // Used to check if user submits an empty string. revursive

            return input;
        }//end getString

        public static void printMenu(){
            Console.WriteLine("\nHello, welcome to your console newsletter.");
            Console.WriteLine("Please select from the following options.");
            Console.WriteLine("1. Top 10 Global Headlines.");
            Console.WriteLine("2. Search by keyword.");
            Console.WriteLine("3. Detailed search...");
            Console.WriteLine("4. Global headlines analytics");
            Console.WriteLine("Press X to exit the program.");
        }//end printMenu

        public static Boolean checkChoice(char choice){
            Boolean valid = false;

            char[] options = {'1', '2', '3', 'X'};

            foreach(char element in options){
                if(choice == element){
                    valid = true;
                }
            }

            return valid;
        }



    } //end Generic
}//end namespace 
