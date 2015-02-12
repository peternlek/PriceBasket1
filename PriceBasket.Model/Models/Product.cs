using System.Collections.Generic;
using System.Linq;

namespace PriceBasket.Model.Models
{
    public class Product : IProduct
    {
        public Product()
        {
            Discounts = new List<IDiscount>();
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double DiscountValue { get; set; }
        public IList<IDiscount> Discounts { get; set; }

        public string[] CheckForDiscounts(IEnumerable<IProduct> basket)
        {
            var validDiscounts = Discounts.Where(x => x.IsValid(basket)).ToArray();
            return validDiscounts.Any()
                ? validDiscounts.Select(x => x.DiscountDetails(this)).ToArray()
                : new string[] {};
        }
    }
}
