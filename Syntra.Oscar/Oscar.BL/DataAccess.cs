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

                user.userName = line.Substring(0, (line.IndexOf('/')));
                user.passWord = line.Substring((line.IndexOf('/') + 1));
                user.passWord = user.passWord.Substring(0, user.passWord.IndexOf('!'));
                user.admin = Convert.ToBoolean(line.Substring(line.IndexOf('!') + 1));

                userList.Add(user);
            }

            return userList;
        }
    }
}
