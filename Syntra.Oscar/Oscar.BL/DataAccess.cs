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
        string userFile = @"c:\temp\usersOscar.txt";

        public DataAccess()
        {

        }

        public void CheckIfUserDatabaseExist()
        {
            bool fileExist = File.Exists(userFile);

            if (fileExist) // Checking if userfile exists. If not, creating one with admin-user in it.
            {
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

        public List<User> LoadUsers()
        {
            CheckIfUserDatabaseExist();
            
            List<User> userList = new List<User>();
            /*
            foreach (var line in File.ReadAllLines(userFile))
            {
                User user = new User();

                string temporaryStorage = line;

                user.userId = temporaryStorage.Substring(0, (temporaryStorage.IndexOf('/')));
                temporaryStorage = temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1);
                user.UserPassword = temporaryStorage.Substring(0, temporaryStorage.IndexOf('/'));
                temporaryStorage = temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1);
                user.UserAdmin = Convert.ToBoolean(temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1));

                userList.Add(user);
            }
            */
            return userList;
            
        }

        public void SaveUsers(List<User> userList)
        {
            File.Delete(userFile);
            StreamWriter file = new StreamWriter(File.Create(userFile));
            file.Close();

            using (StreamWriter sw = new StreamWriter(userFile))
            {
                foreach (var user in userList)
                {
                    string userText = user.userId + "/" + user.UserPassword + "/" + user.UserAdmin;
                    sw.WriteLine(userText);
                }
            }
        }
    }
}
