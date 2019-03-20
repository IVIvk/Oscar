using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class User
    {
        public string userName { get; set; }
        public string passWord { get; set; }
        public bool admin { get; set; } = false;

        public User()
        {
            
        }
    }
}
