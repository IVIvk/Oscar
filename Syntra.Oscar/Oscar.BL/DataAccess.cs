using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class DataAccess
    {
        public DataAccess()
        {

        }

        public void CheckIfDatabaseExist()
        {
            string userFile = @"c:\temp\usersOscar.txt";
            List<User> userList = new List<User>();

            bool fileExist = File.Exists(userFile);

            if (fileExist) // Checking if userfile exists. If not, creating one with admin-user in it.
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

        public List<User> LoadUsers(string userFile)
        {
            List<User> userList = new List<User>();

            foreach (var line in File.ReadAllLines(userFile))
            {
                User user = new User();

                string temporaryStorage = line;

                user.userName = temporaryStorage.Substring(0, (temporaryStorage.IndexOf('/')));
                temporaryStorage = temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1);
                user.passWord = temporaryStorage.Substring(0, temporaryStorage.IndexOf('/'));
                temporaryStorage = temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1);
                user.admin = Convert.ToBoolean(temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1));

                userList.Add(user);
            }

            return userList;
        }

        public void SaveUsers(List<User> userList, string userFile)
        {
            File.Delete(userFile);
            StreamWriter file = new StreamWriter(File.Create(userFile));
            file.Close();

            using (StreamWriter sw = new StreamWriter(userFile))
            {
                foreach (var user in userList)
                {
                    string userText = user.userName + "/" + user.passWord + "/" + user.admin;
                    sw.WriteLine(userText);
                }
            }
        }
    }
}
