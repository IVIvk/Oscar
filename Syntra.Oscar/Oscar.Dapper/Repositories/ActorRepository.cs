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
        /////////////////////////////////////////
        // Functions.

        // This function gets all the properties that belong to all the Actors objects form the database.
        // It places these properties in an Actors object.
        // It places these objects into an IEnumerable "list".
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
        
        
        public IEnumerable<ActorsInFilms> GetActorsInFilms()
        {
            using (var connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                return connection.Query<ActorsInFilms>
                    (@"
                        SELECT ActorId, FilmId
                        FROM ActorsinFilms
                    ");
            }
        }
        
        // This function inserts the properties of the parameter (Actors object) 
        // Into the database as a new entry in the Actors table.
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
                    FirstName = actor.FirstName,
                    LastName = actor.LastName
                });
            }
        }

        public void DeleteActor (Actors actor)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    DELETE FROM Actors
                    WHERE ActorId = @ActorId
                ", new
                {
                    ActorId = actor.ActorId
                });
            }
        }

        public void UpdateActor (Actors actor)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute(@"
                    UPDATE Actors
                    SET FirstName = @ActorFirstName, LastName = @ActorLastName 
                    WHERE ActorId = @ActorId
                ", new
                {
                    ActorFirstName = actor.FirstName,
                    ActorLastName = actor.LastName,
                    ActorId = actor.ActorId
                });
            }
        }

        // This function inserts a link between a film and an actor inside the ActorsInFilms table.
        public void InsertLinkActorAndFilm(Actors actor, Nullable<Guid> guidFilm)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        INSERT INTO ActorsInFilms(ActorId, FilmId)
                        VALUES (@ActorId, @FilmId)                      
                        ", new
                    {
                        ActorId = actor.ActorId,
                        FilmId = guidFilm
                    });
            }
        }

        // This function deletes the links between a film and its genres inside the GenresInFilms table.
        public void DeleteLinkActorAndFilm(Nullable<Guid> FilmId, Actors actor)
        {
            using (SqlConnection connection = new SqlConnection(Connection.Instance.ConnectionString))
            {
                connection.Execute
                    (@"
                        DELETE FROM ActorsInFilms
                        WHERE FilmId = @FilmId AND ActorId = @ActorId
                        ", new
                    {
                        FilmId = FilmId,
                        ActorId = actor.ActorId
                    });
            }
        }
    }
}
