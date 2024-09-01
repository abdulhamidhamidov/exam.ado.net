using Npgsql;
using shoping.models;

namespace shoping.Service;

public class ProductCatigoryService : IProductCatigoryService
{
    #region CreateTable

    public static void CreateTable()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandsCatigory.CreateTable, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandsCatigory.DropTable, connection))
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
    
    #region GetProductCatigories

    public List<ProductCatigory> GetProductCatigories()
    {
        try
        {
            List<ProductCatigory> productCatigories = new();
            using (NpgsqlConnection connection=new NpgsqlConnection(SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsCatigory.ReadCatigory;
                    using (NpgsqlDataReader reader= command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductCatigory productCatigory=new ProductCatigory();
                            productCatigory.id = reader.GetInt32(0);
                            productCatigory.name = reader.GetString(1);
                            productCatigory.desc1 = reader.GetString(2);
                            productCatigory.created_at = reader.GetDateTime(3);
                        
                            productCatigories.Add(productCatigory);
                        }
                    }

                    return productCatigories;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    #endregion

    #region GetProductCatigoryById

    public ProductCatigory GetProductCatigoryById(int id)
    {
        try
        {
            ProductCatigory productCatigory=new ProductCatigory();
            using (NpgsqlConnection connection=new (SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsCatigory.ReadCatigoryById;
                    command.Connection = connection;
                    using (NpgsqlDataReader reader=command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productCatigory.id = reader.GetInt32(0);
                            productCatigory.name = reader.GetString(1);
                            productCatigory.desc1 = reader.GetString(2);
                            productCatigory.created_at = reader.GetDateTime(3);
                        }

                        return productCatigory;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region CreateProductCatigopry

    public bool CreateProductCatigopry(ProductCatigory productCatigory)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection = new (SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command=connection.CreateCommand())
                {
                    command.CommandText = SqlCommandsCatigory.InsertCatigory;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@id", productCatigory.id);
                    command.Parameters.AddWithValue("@name", productCatigory.name);
                    command.Parameters.AddWithValue("@desc1", productCatigory.desc1);
                    command.Parameters.AddWithValue("@created_at", productCatigory.created_at);

                    res = command.ExecuteNonQuery();
                }
            }

            return res > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    #endregion

    #region UpdateProductCatigory

    public bool UpdateProductCatigory(ProductCatigory productCatigory)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection = new (SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandsCatigory.UpdateCatigory;

                    command.Parameters.AddWithValue("@id", productCatigory.id);
                    command.Parameters.AddWithValue("@name", productCatigory.name);
                    command.Parameters.AddWithValue("@desc1", productCatigory.desc1);
                    command.Parameters.AddWithValue("@created_at", productCatigory.created_at);

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
  
    #region DeleteProductCatigory
    public bool DeleteProductCatigory(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new (SqlCommandsCatigory.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandsCatigory.DeleteCatigory;
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

file class SqlCommandsCatigory
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=12345;";

    public const string CreateTable = @"CREATE TABLE If not exists Product_Catigory (
                                      id SERIAL PRIMARY KEY, 
                                      name VARCHAR(50) unique NOT NULL,
                                      desc1 VARCHAR(50) not null,
                                      created_at date)";

    public const string DropTable = "DROP TABLE if exists Product_Catigory ";

    public const string InsertCatigory = @"Insert into Catigorys(name,desc1 ,created_at)
                                       values(@name,@desc1,@created_at)";

    public const string UpdateCatigory  =
        "Update Catigorys set name=@name,desc1=@desc1,created_at=@created_at where id=@id";

    public const string DeleteCatigory  = "Delete from Catigorys where id=@id";
    public const string ReadCatigory  = "Select * from Catigorys";
    public const string ReadCatigoryById = "Select * from Catigorys where id=@id";
}