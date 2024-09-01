using Npgsql;
using shoping.models;

namespace shoping.Service;

public class UserAddressService : IUserAddressService
{
    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=new NpgsqlCommand(SqlCommandAddress.CreateTable,connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
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
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=new (SqlCommandAddress.DropTable,connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion

    #region getUsersAddress
    public List<UserAddress> getUsersAddress()
    {
        try
        {
            List<UserAddress> userAddresses=new List<UserAddress>();
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandAddress.ReadUserAddress;
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserAddress userAddress = new ();
                            userAddress.id = reader.GetInt32(0);
                            userAddress.user_id = reader.GetInt32(1);
                            userAddress.address = reader.GetString(2);
                            userAddress.city = reader.GetString(3);
                            userAddress.postal_code = reader.GetString(4);
                            userAddress.country = reader.GetString(5);
                            userAddress.telephone = reader.GetString(6);
                            
                            userAddresses.Add(userAddress);
                        }
                    }
                }
            }

            return userAddresses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    #endregion

    #region GetUserAddressById
    public UserAddress GetUserAddressById(int id)
    {
        try
        {
            UserAddress userAddress=new ();
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandAddress.ReadUserAddressById;
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userAddress.id = reader.GetInt32(0);
                            userAddress.user_id = reader.GetInt32(1);
                            userAddress.address = reader.GetString(2);
                            userAddress.city = reader.GetString(3);
                            userAddress.postal_code = reader.GetString(4);
                            userAddress.country = reader.GetString(5);
                            userAddress.telephone = reader.GetString(6);
                              
                        }
                    }
                    return userAddress;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    #endregion

    #region CreateUserAddress

    public bool CreateUserAddress(UserAddress userAddress)
    {
        try
        {
            int res;
            using (NpgsqlConnection connection = new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandAddress.InsertUserAddress;

                    command.Parameters.AddWithValue("@id", userAddress.id);
                    command.Parameters.AddWithValue("@user_id", userAddress.user_id);
                    command.Parameters.AddWithValue("@address", userAddress.address);
                    command.Parameters.AddWithValue("@city", userAddress.city);
                    command.Parameters.AddWithValue("@postal_code", userAddress.postal_code);
                    command.Parameters.AddWithValue("@country", userAddress.country);
                    command.Parameters.AddWithValue("@telephone", userAddress.telephone);

                    res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    #endregion

    #region UpdateUserAddress

    public bool UpdateUserAddress(UserAddress userAddress)
    {
        try
        {
            int res;
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandAddress.UpdateUserAddress;
                    command.Connection = connection;
                    
                    command.Parameters.AddWithValue("@id", userAddress.id);
                    command.Parameters.AddWithValue("@user_id", userAddress.user_id);
                    command.Parameters.AddWithValue("@address", userAddress.address);
                    command.Parameters.AddWithValue("@city", userAddress.city);
                    command.Parameters.AddWithValue("@postal_code", userAddress.postal_code);
                    command.Parameters.AddWithValue("@country", userAddress.country);
                    command.Parameters.AddWithValue("@telephone", userAddress.telephone);

                    res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    

    #endregion


    #region DeleteUserAddress

    public bool DeleteUserAddress(int id)
    {
        try
        {
            using (NpgsqlConnection connection=new (SqlCommandAddress.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandAddress.DeleteUserAddress;
                    command.Parameters.AddWithValue("@id", id);
                    int res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    #endregion
}


file class SqlCommandAddress
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=12345;";
    
    public const string CreateTable = @"CREATE TABLE If not exists user_address (
                                      id SERIAL PRIMARY KEY, 
                                      user_id INT REFERENCES users(Id),
                                      address VARCHAR(50) unique NOT NULL,
                                      city VARCHAR(50) not null,
                                      postal_code VARCHAR(50)  not null,
                                      country varchar(50)  not null,
                                      telephone varchar(50) unique not null)";

    public const string DropTable = "DROP TABLE if exists user_address";

    public const string InsertUserAddress = @"Insert into user_address(user_id,address,city,postal_code,country,telephone)
                                       values(@user_id,@address,@city,@postal_code,@country,@telephone)";

    public const string UpdateUserAddress =
        "Update users set user_id=@user_id,address=@address,city=@city,postal_code=@postal_code,country=@country,telephone=@telephone where id=@id";

    public const string DeleteUserAddress = "Delete from user_address where id=@id";
    public const string ReadUserAddress = "Select * from user_address";
    public const string ReadUserAddressById = "Select * from user_address where id=@id";
}