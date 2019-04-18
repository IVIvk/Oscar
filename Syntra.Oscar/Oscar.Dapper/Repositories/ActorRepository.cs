using Dapper;
using Oscar.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Oscar.Dapper.Repositories
{
    public class ActorRepository
    {
        public IEnumerable<Actors> GetActors()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<Actors>
                    (@" 
                        SELECT ActorId, FirstName, LastName
                        FROM Actors
                    ");
            }
        }

        public void InsertActor(Actors actor)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    INSERT INTO Actors(ActorId, FirstName, LastName)
                    VALUES (@ActorId, @FirstName, @LastName)
                ", new
                {
                    ActorId = actor.ActorId,
                    FirstName = actor.ActorFirstName,
                    LastName = actor.ActorLastName
                });
            }
        }
    }
}
