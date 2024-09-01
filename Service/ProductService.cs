using shoping.models;
using Npgsql;
namespace shoping.Service;

public class ProductService: IProductService
{


    #region CreateDatabase
    
    public static void CreateDatabase()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandProduct.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandProduct.CreateDatabase, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandProduct.DefaultConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandProduct.DropDatabase, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandProduct.CreateTable, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SqlCommandProduct.DropTable, connection))
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

    #region GetProducts

    public List<Product> GetProducts()
    {
        try
        {
            List<Product> products = new();
            using (NpgsqlConnection connection = new(SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SqlCommandProduct.ReadProducts;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product= new ();
                            product.id = reader.GetInt32(0);
                            product.name = reader.GetString(1);
                            product.desc1 = reader.GetString(2);
                            product.catigory_id = reader.GetString(3);
                            product.price = reader.GetInt32(4);
                            product.created_at = reader.GetDateTime(5);
                            //user.Id = Convert.ToInt32(reader["id"]); 

                            products.Add(product);
                        }
                    }
                }

                return products;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion

   

    #region GetProductById

    public Product GetProductById(int id)
    {
        try
        {
            Product product = new();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = SqlCommandProduct.ReadProductById;
                    command.Parameters.AddWithValue("@id", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product.id = reader.GetInt32(0);
                            product.name = reader.GetString(1);
                            product.desc1 = reader.GetString(2);
                            product.catigory_id = reader.GetString(3);
                            product.price = reader.GetInt32(4);
                            product.created_at = reader.GetDateTime(5);
                        }
                        return product;
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

    #region CreateProduct

    

   
    public bool CreateProduct(Product product)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection = new (SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command= new ())
                {
                    command.CommandText = SqlCommandProduct.InsertProduct;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@id", product.id);
                    command.Parameters.AddWithValue("@name", product.name);
                    command.Parameters.AddWithValue("@desc1", product.desc1);
                    command.Parameters.AddWithValue("@catrigory_id", product.catigory_id);
                    command.Parameters.AddWithValue("@price", product.price);
                    command.Parameters.AddWithValue("@created_at", product.created_at);

                    res = command.ExecuteNonQuery();
                }

            }
            return res > 0 ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    #endregion

    #region UpdateProduct
    
    public bool UpdateProduct(Product product)
    {
        try
        {
            int res = 0;
            using (NpgsqlConnection connection=new (SqlCommandProduct.ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new ())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandProduct.UpdateProduct;
                    
                    command.Parameters.AddWithValue("@id", product.id);
                    command.Parameters.AddWithValue("@name", product.name);
                    command.Parameters.AddWithValue("@desc1", product.desc1);
                    command.Parameters.AddWithValue("@catrigory_id", product.catigory_id);
                    command.Parameters.AddWithValue("@price", product.price);
                    command.Parameters.AddWithValue("@created_at", product.created_at);

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

    #region DeleteProduct
    
    public bool DeleteProduct(int id)
    {
        try
        {
            using (NpgsqlConnection connection=new (SqlCommandProduct.ConnectionString))
            {
                using (NpgsqlCommand command = new ())
                {
                    command.Connection = connection;
                    command.CommandText = SqlCommandProduct.DeleteProducts;
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

file class SqlCommandProduct
{
    public const string DefaultConnectionString =
        "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";

    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=user_db;Username=postgres;Password=12345;";

    public const string CreateDatabase = "CREATE DATABASE  user_db";
    public const string DropDatabase = "DROP DATABASE  user_db with(force)";

    public const string CreateTable = @"CREATE TABLE If not exists Products (
                                      id SERIAL PRIMARY KEY, 
                                      name VARCHAR(50) unique NOT NULL,
                                      catigory_id INT references Product_Catigory(id),
                                      desc1 VARCHAR(50) not null,
                                      price int,
                                      created_at date )";

    public const string DropTable = "DROP TABLE if exists Products";

    public const string InsertProduct = @"Insert into users(name,desc1,catigory_id,price,created_at)
                                       values(@name,@desc1,@catigory_id,@price,@created_at)";

    public const string UpdateProduct =
        "Update users set name=@name,desc1=@desc1,catigory_id=@catigory_id,price=@price,created_at=@created_at where id=@id";

  public const string DeleteProducts = "Delete from users where id=@id";
    public const string ReadProducts = "Select * from users";
    public const string ReadProductById = "Select * from users where id=@id";  
}
