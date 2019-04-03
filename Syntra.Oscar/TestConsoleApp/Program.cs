using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oscar.BL;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine(" +----------------+");
            Console.WriteLine(" | This is a test |");
            Console.WriteLine(" +----------------+");
            Console.WriteLine("");

            // TEST for the Genres Class.
            Genres genreName1 = new Genres();
            List<string> listOfGenres = new List<string>();

            string userInput = string.Empty;

            // Use AddNewGenre method to interact with the Genres class.
            // Add a  GenreName.
            genreName1.AddNewGenre("Horror");

            // Add this GenreName to the listOfGenres.
            listOfGenres.Add(genreName1.GenreName);

            Console.Write("Please enter a genre: ");
            userInput = Console.ReadLine();

            genreName1.AddNewGenre(userInput);
            listOfGenres.Add(genreName1.GenreName);            

            // Print all the items from the list
            foreach (string item in listOfGenres)
            {
                Console.WriteLine($" {item}");
            }

            Console.ReadKey();
        }
    }
}
