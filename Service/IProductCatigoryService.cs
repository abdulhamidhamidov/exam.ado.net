using shoping.models;

namespace shoping.Service;

public interface IProductCatigoryService
{
    List<ProductCatigory> GetProductCatigories();
    ProductCatigory GetProductCatigoryById(int id);
    bool CreateProductCatigopry(ProductCatigory ProductCatigory);
    bool UpdateProductCatigory(ProductCatigory productCatigory);
    bool DeleteProductCatigory(int id);
}