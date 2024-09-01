using shoping.models;

namespace shoping.Service;

public interface IProductService
{
    List<Product> GetProducts();
    Product GetProductById(int id);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product Product);
    bool DeleteProduct(int id);
}