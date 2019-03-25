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
                User newUser = new User();
                string newUsername;
                string newPassword;
                bool usernameExist = false;
                bool passwordExist = false;

                do 
                {
                    Console.Clear();
                    usernameExist = false;

                    Console.Write("Geef een gebruikersnaam: ");
                    newUsername = Console.ReadLine();

                    foreach (User user in userList)
	                {
                        if (user.userName == newUsername)
                        {
                            usernameExist = true;
                        }
	                }
                }
                while (usernameExist);

                do
	            {
                    Console.Clear();
                    passwordExist = false;

                    Console.Write("Geef een wachtwoord: ");
                    newPassword = Console.ReadLine();
                    Console.Write("Herhaal het wachtwoord: ");

                    if (!(Console.ReadLine() == newPassword))
	                {
                        passwordExist = true;
                        Console.WriteLine("De invoer komt niet overeen. Druk op enter om opnieuw te proberen.");
                        Console.ReadLine();
	                }
	            } while (passwordExist);

                newUser.userName = newUsername;
                newUser.passWord = newPassword;

                userList.Add(newUser);
            }
            
        }
    }
}
