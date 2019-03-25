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

        public List<User> LoadUsers(string userFile)
        {
            List<User> userList = new List<User>();

            foreach (var line in File.ReadAllLines(userFile))
            {
                User user = new User();

                string temporaryStorage = line;

                user.userName = temporaryStorage.Substring(0, (temporaryStorage.IndexOf('/')));
                temporaryStorage = temporaryStorage.Substring((temporaryStorage.IndexOf('/') + 1));
                user.passWord = temporaryStorage.Substring(0, temporaryStorage.IndexOf('/'));
                temporaryStorage = temporaryStorage.Substring((temporaryStorage.IndexOf('/') + 1));
                user.admin = Convert.ToBoolean(temporaryStorage.Substring(temporaryStorage.IndexOf('/') + 1));

                userList.Add(user);
            }

            return userList;
        }
    }
}
