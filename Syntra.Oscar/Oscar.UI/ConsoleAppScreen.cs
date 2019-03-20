using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.UI
{
    public class ConsoleAppScreen
    {
        public ConsoleAppScreen()
        {

        }

        public void LoginScreen()
        {
            string username;
            string password;

            bool registratedUser = false;

            Console.WriteLine("Welkom bij Oscar");
            Console.Write("Ben je al geregistreerd (j/n): ");

            if (Console.ReadLine().ToLower() == "j")
            {
                registratedUser = true;
            }

            if (registratedUser)
            {
                Console.Write("Gebruikersnaam: ");
                username = Console.ReadLine();

                Console.Write("Wachtwoord: ");
                password = Console.ReadLine();
            }
            else
            {

            }
        }
    }
}
