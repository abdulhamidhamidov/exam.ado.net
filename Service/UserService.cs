using Npgsql;
using shoping.models;
using shoping.Service;

namespace Tests.Services;

class UserService : IUserService
{
    #region CreateDatabase

    public static void CreateDatabase()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateDatabase, connection))
                {
                    // cmd.Connection = connection;
                    // cmd.CommandText = SqlCommands.CreateDatabase;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion

    #region DropDatabase

    public static void DropDatabase()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropDatabase, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion

    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.CreateTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion

    #region DropTable

    public static void DropTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommands.DropTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion


    #region GetUsers

    public List<User> GetUsers()
    {
        try
        {
            List<User> users = new();
            using (NpgsqlConnection connection = new(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SqlCommands.ReadUsers;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Age = reader.GetInt32(1);
                            user.UserName = reader.GetString(2);
                            user.Password = reader.GetString(3);
                            user.Email = reader.GetString(4);

                            //user.Id = Convert.ToInt32(reader["id"]); 

                            users.Add(user);
                        }
                    }
                }

                return users;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

    #region GetUserById

    public User GetUserById(int id)
    {
        try
        {
            User user = new();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SqlCommands.ReadUserById;
                    command.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(0);
                            user.Age = reader.GetInt32(1);
                            user.UserName = reader.GetString(2);
                            user.Password = reader.GetString(3);
                            user.Email = reader.GetString(4);
                        }
                        return user;
                    }
                }

            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

    #region CreateUser

    public bool CreateUser(User user)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = SqlCommands.InsertUser;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@username", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@email", user.Email);

                    res = command.ExecuteNonQuery();
                }
            }

            return res > 0;
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    #endregion

    #region UpdateUser

    public bool UpdateUser(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommands.UpdateUser;

                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@username", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@email", user.Email);

                    int res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    #endregion

    #region DeleteUser

    public bool DeleteUser(int id)
    {
        try
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection=conn;
                    cmd.CommandText = SqlCommands.DeleteUser;
                    cmd.Parameters.AddWithValue("@id", id);
                    int res = cmd.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    #endregion
}

file class SqlCommands
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=12345;";

    public const string CreateDatabase = "CREATE DATABASE  user_db";
    public const string DropDatabase = "DROP DATABASE  user_db with(force)";

    public const string CreateTable = @"CREATE TABLE If not exists users (
                                      id SERIAL PRIMARY KEY, 
                                      age INT,
                                      username VARCHAR(50) unique NOT NULL,
                                      password VARCHAR(50) not null,
                                      email VARCHAR(50) unique not null)";

    public const string DropTable = "DROP TABLE if exists users";

    public const string InsertUser = @"Insert into users(age,username,password ,email)
                                       values(@age,@username,@password,@email)";

    public const string UpdateUser =
        "Update users set age=@age,username=@username,password=@password,email=@email where id=@id";

    public const string DeleteUser = "Delete from users where id=@id";
    public const string ReadUsers = "Select * from users";
    public const string ReadUserById = "Select * from users where id=@id";
}