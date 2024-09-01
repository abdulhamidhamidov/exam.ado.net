using System.Data;
using Npgsql;
using shoping.models;

namespace shoping.Service;

public class UserPaymentService : IUserPaymentService
{
    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection= new(SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new (SqlCommandsPayment.CreateTable,connection))
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
            using (NpgsqlConnection connection= new(SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new (SqlCommandsPayment.DropTable,connection))
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

    #region GetUserPayments

    public List<UserPayment> GetUserPayments()
    {
        try
        {
            List<UserPayment> userPayments = new List<UserPayment>();
            using (NpgsqlConnection connection = new(SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsPayment.ReadUserPayments;
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserPayment userPayment = new UserPayment();

                            userPayment.id = reader.GetInt32(0);
                            userPayment.user_id = reader.GetInt32(1);
                            userPayment.payment_type = reader.GetString(2);
                            userPayment.provider = reader.GetString(3);
                            userPayment.account_no = reader.GetInt32(4);
                            userPayment.expiry = reader.GetDateTime(5);
                            userPayments.Add(userPayment);
                        }
                    }
                }
            }

            return userPayments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

    #region GetUserPaymentById

    public UserPayment GetUserPaymentById(int id)
    {
        try
        {
            UserPayment userPayment = new UserPayment();
            using (NpgsqlConnection connection=new (SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsPayment.ReadUserPaymentById;
                    command.Parameters.AddWithValue("@id", id);
                    using (NpgsqlDataReader reader=command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            userPayment.id = reader.GetInt32(0);
                            userPayment.user_id = reader.GetInt32(1);
                            userPayment.payment_type = reader.GetString(2);
                            userPayment.provider = reader.GetString(3);
                            userPayment.account_no = reader.GetInt32(4);
                            userPayment.expiry = reader.GetDateTime(5);
                        }

                        return userPayment;
                    }
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

    #region CreateUserPayment

    public bool CreateUserPayment(UserPayment userPayment)
    {
        try
        {
            int res;
            using (NpgsqlConnection connection = new (SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsPayment.InsertUserPayment;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@id", userPayment.id);
                    command.Parameters.AddWithValue("@user_id", userPayment.user_id);
                    command.Parameters.AddWithValue("@payment_type", userPayment.payment_type);
                    command.Parameters.AddWithValue("@provider", userPayment.provider);
                    command.Parameters.AddWithValue("@account_no", userPayment.account_no);
                    command.Parameters.AddWithValue("@expiry", userPayment.expiry);
                    res = command.ExecuteNonQuery();
                }
            }

            return res > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

    #region UpdateUserPayment

    public bool UpdateUserPayment(UserPayment userPayment)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection = new (SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=new ())
                {
                    command.CommandText = SqlCommandsPayment.UpdateUserPayment;
                    command.Connection = connection;
                    
                    command.Parameters.AddWithValue("@id", userPayment.id);
                    command.Parameters.AddWithValue("@user_id", userPayment.user_id);
                    command.Parameters.AddWithValue("@payment_type", userPayment.payment_type);
                    command.Parameters.AddWithValue("@provider", userPayment.provider);
                    command.Parameters.AddWithValue("@account_no", userPayment.account_no);
                    command.Parameters.AddWithValue("@expiry", userPayment.expiry);
                    res = command.ExecuteNonQuery();
                    return res>0;
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

    #region DeleteUserPayment

    public bool DeleteUserPayment(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new(SqlCommandsPayment.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandsPayment.DeleteUserPayment;
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

#region SqlCommandsPayment

file class SqlCommandsPayment
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=12345;";

    public const string CreateDatabase = "CREATE DATABASE  user_db";
    public const string DropDatabase = "DROP DATABASE  user_db with(force)";

    public const string CreateTable = @"CREATE TABLE If not exists UserPayment (
                                      id SERIAL PRIMARY KEY, 
                                      user_id INT references users(Id),
                                      payment_type VARCHAR(50) unique NOT NULL,
                                      provider VARCHAR(50) not null,
                                      account_no int,
                                      expiry date)";

    public const string DropTable = "DROP TABLE if exists UserPayment";

    public const string InsertUserPayment = @"Insert into UserPayment(user_id,payment_type,provider ,account_no,expiry)
                                       values(@user_id,@payment_type,@provider ,@account_no,@expiry)";

    public const string UpdateUserPayment =
        "Update UserPayment set user_id=@user_id,payment_type=@payment_type,provider=@provider ,account_no=@account_no,expiry=@expiry where id=@id";

    public const string DeleteUserPayment = "Delete from UserPayment where id=@id";
    public const string ReadUserPayments = "Select * from UserPayment";
    public const string ReadUserPaymentById = "Select * from UserPayment where id=@id";
}
#endregion