using WebAPI.Model;

namespace WebAPI.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}