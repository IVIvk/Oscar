using Oscar.Dapper;
using Oscar.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Oscar.UI.WPF
{
    class DatabaseManager
    {
        private static readonly DatabaseManager _manager = new DatabaseManager();

        public static DatabaseManager Instance => _manager;

        public UserRepository UserRepository => new UserRepository();
        public ActorRepository ActorRepository => new ActorRepository();
        public GenreRepository GenreRepository => new GenreRepository();
        public FilmRepository FilmRepository => new FilmRepository();

        private DatabaseManager()
        {            
            Connection.Instance.SetConnection(@"Server=localhost\SQLExpress;Initial Catalog=Oscar;Integrated Security=True");
        }
    }
}
