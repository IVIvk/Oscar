using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Oscar.BL;

namespace Oscar.UI
{
    public class ConsoleAppScreen
    {
        List<User> userList = new List<User>();

        public ConsoleAppScreen()
        {
            string userFile = @"c:\temp\usersOscar.txt";
            bool fileExist = File.Exists(userFile);

            if (fileExist)
            {
                userList = new DataAccess().LoadUsers(userFile);
            }
            else
            {
                string defaultText = @"Admin/AdminPassword/true";
                StreamWriter file = new StreamWriter(File.Create(userFile));
                file.Close();

                using (StreamWriter sw = new StreamWriter(userFile))
                {
                    sw.WriteLine(defaultText);
                }
            }
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
