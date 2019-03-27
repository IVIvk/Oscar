using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    class Actors
    {
        // Data members.
        private string actorFirstName = string.Empty;
        private string actorLastName = string.Empty;
        // List of films that the actor has appeared in. (To be added)

        /////////////////////////////////////////
        // Access to the data members.
        public string ActorFirstName
        {
            get { return actorFirstName; }
            //set { actorFirstName = value; }
        }

        public string ActorLastName
        {
            get { return actorLastName; }
            //set { actorLastName = value; }
        }

        /////////////////////////////////////////
        // Functions.

        // This function adds user input to the Actors object.
        public void AddNewActor(string inputFirstName, string inputLastName)
        {
            actorFirstName = inputFirstName;
            actorLastName = inputLastName;
        }        
    }
}
