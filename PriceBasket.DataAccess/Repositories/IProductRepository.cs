using PriceBasket.Model.Models;

namespace PriceBasket.DataAccess.Repositories
{
    public interface IProductRepository
    {
        IProduct[] GetProducts(string[] products);
    }
}
