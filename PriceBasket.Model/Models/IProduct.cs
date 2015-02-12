using System.Collections.Generic;

namespace PriceBasket.Model.Models
{
    public interface IProduct
    {
        string Name { get; set; }
        double Price { get; set; }
        int Quantity { get; set; }
        IList<IDiscount> Discounts { get; set; }
        double DiscountValue { get; set; }
        string[] CheckForDiscounts(IEnumerable<IProduct> basket);
    }
}
