﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oscar.BL;

namespace Oscar.UI.WPF.SingletonClasses
{
    public class SingletonGenre
    {
        // Singleton class.
        //
        /////////////////////////////////
        // Data members.
        private static readonly SingletonGenre _genre = new SingletonGenre();

        private Nullable<Guid> genreId;
        private string genreName = string.Empty;
        
        /////////////////////////////////
        // Properties.        
        public Nullable<Guid> GenreId
        {
            get { return genreId; }
            set { genreId = value; }
        }

        public string GenreName
        {
            get { return genreName; }
            set { genreName = value; }
        }

        /////////////////////////////////
        // Singleton stuff. 
        public static SingletonGenre OnlyInstanceOfGenre
        {
            get { return _genre; }
        }

        private SingletonGenre()
        {

        }
    }
}
