using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class Actors
    {
        // Data members.
        private Nullable<Guid> actorId;
        private string actorFirstName = string.Empty;
        private string actorLastName = string.Empty;
        // List of films that the actor has appeared in. (To be added)

        /////////////////////////////////////////
        // Access to the data members.
        public Nullable<Guid> ActorId
        {
            get { return actorId; }
            set
            {
                if (actorId == null)
                {
                    actorId = value;
                }                
            }
        }

        public string FirstName
        {
            get { return actorFirstName; }
            set { actorFirstName = value; }
        }
      
        public string LastName
        {
            get { return actorLastName; }
            set { actorLastName = value; }
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
