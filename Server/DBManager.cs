using Gamelogic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DBManager
    {
        public struct UserState
        {
            public MoveType? choice;
            public int score;
        }
        private static NpgsqlConnection connection;
        private static string connString = "Server=10.0.0.1;Port=9999;User Id=postgres;Password=postgres;Database=postgres;";
        public static void Init()
        {
            //connection = new NpgsqlConnection(connString);
            //connection.Open();
            //TODO : Close
        }

        public static void InsertUser(string userID)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO games VALUES (@userID, NULL, 0);";
                command.Parameters.AddWithValue("userID", userID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }
        public static void NullChoices(string userIDA, string userIDB)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE games SET choice=NULL WHERE userid=@userIDA OR userid=@userIDB;";
                command.Parameters.AddWithValue("userIDA", userIDA);
                command.Parameters.AddWithValue("userIDB", userIDB);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }
        public static void InsertChoice(string userID, MoveType choice)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE games SET choice=@choice WHERE userid=@userID;";
                command.Parameters.AddWithValue("userID", userID);
                command.Parameters.AddWithValue("choice", (int)choice);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }

        public static void IncrementScore(string userID)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE games SET score=score+1 WHERE userid=@userID;";
                command.Parameters.AddWithValue("userID", userID);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }

        public static void Delete(string userIDA, string userIDB)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM games WHERE userid=@userIDA OR userid=@userIDB;";
                command.Parameters.AddWithValue("@userIDA", userIDA);
                command.Parameters.AddWithValue("@userIDB", userIDB);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }

        public static void Delete(string userIDA)
        {
            try
            {
                var connection = new NpgsqlConnection(connString);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM games WHERE userid=@userIDA";
                command.Parameters.AddWithValue("@userIDA", userIDA);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
        }

        public static UserState getUserState(string userID)
        {
            var connection = new NpgsqlConnection(connString);
            connection.Open();
            UserState userState = new UserState();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT choice, score FROM games WHERE userid=@userID;";
            command.Parameters.AddWithValue("@userID", userID);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetValue(0) == DBNull.Value)
                    userState.choice = null;
                else
                    userState.choice = (MoveType?)reader.GetInt32(0);
                userState.score = reader.GetInt32(1);
            }
            connection.Close();
            return userState;
        }
    }
}
