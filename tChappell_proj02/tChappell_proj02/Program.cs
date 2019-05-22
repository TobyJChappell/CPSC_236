/**
 * @author Toby Chappell
 * @student_id 2312642
 * @email tchappell@chapman.edu
 * @course CPSC-236-03
 * @assignment 2
 * 
 *  Asks the user a series of questions, which require either a purely string response or a purely integer response. 
 *  The responses are then checked if they are valid and outputted back to the console.
 *  The application continues asking these questions until the user closes the application.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tChappell_proj02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I'm going to ask you a series of questions, feel free to type \"exit\" to leave the program at any time.");
            string input = "";
            while(input != "exit" && input != "-1")
            {
                input = WriteQuestion();
                if(input != "exit")
                {
                    Console.WriteLine(input);
                }
            }
            Console.WriteLine("Thank you for stopping by, have a great day!");
            Console.ReadLine();
        }

        /**
         * Writes a random question to the console
         * @return string The user's answer to a question
         */
        private static string WriteQuestion()
        {
            Random rnd = new Random();
            int r = rnd.Next(4);
            switch(r)
            {
                case (0):
                    Console.Write("What is your name: ");
                    break;

                case (1):
                    Console.Write("What is your age: ");
                    break;
                case (2):
                    Console.Write("What is your favorite color: ");
                    break;
                case (3):
                    Console.Write("How many pets do you have: ");
                    break;
            }
            string input = Console.ReadLine();
            if(input == "exit")
            {
                return input;
            }
            else if(r == 0 || r == 2)
            {
                return CheckString(input);
            }
            else
            {
                return CheckNum(input) + "";
            }
        }

        /**
         * Determines if an input only consists of letters
         * @param input A user inputted string
         * @return string A valid string
         */
        private static string CheckString(string input)
        {
            bool word;
            do
            {
                word = true;
                for (int x = 0; x < input.Length; x++)
                {
                    if (!char.IsLetter(input[x]))
                    {
                        word = false;
                    }
                }
                if(!word)
                {
                    Console.WriteLine("Please enter a valid word (only letters): ");
                    input = Console.ReadLine();
                }
            } while (!word);          
            return input;
        }

        /**
         * Determines if an input only consists of numbers
         * @param input A user inputted number
         * @return int A valid number
         */
        private static int CheckNum(string input)
        {
            int num = 0;
            while (!int.TryParse(input, out num) && input != "exit")
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }
            if(input == "exit")
            {
                return -1;
            }
            return num;
        }
    }
}