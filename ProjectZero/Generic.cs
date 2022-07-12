using System;

namespace ProjectZero{

    class Generic{

        public static char getChar(){ //getting a single char value from console
            char input = Console.ReadKey().KeyChar; //Reads user input from keyboard
            return input;
        }//end getInpit

        public static string? getString(){

            string input = Console.ReadLine();

            if(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)){
                Console.WriteLine("Don't submit empty strings, try again.");
                getString();
            } // Used to check if user submits an empty string.

            return input;
        }//end getString

    } //end Generic
}//end namespace 
