using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class User
    {
        public string userId { get; set; }
        public string UserPassword { get; set; }
        public bool UserAdmin { get; set; }

        public User()
        {
            UserAdmin = false;
        }
    }
}
