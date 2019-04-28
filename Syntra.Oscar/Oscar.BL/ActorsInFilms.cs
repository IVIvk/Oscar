using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class ActorsInFilms
    {
        private Nullable<Guid> actorId;
        private Nullable<Guid> filmId;

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

        public Nullable<Guid> FilmId
        {
            get { return filmId; }
            set
            {
                if (filmId == null)
                {
                    filmId = value;
                }
            }
        }
    }
}
