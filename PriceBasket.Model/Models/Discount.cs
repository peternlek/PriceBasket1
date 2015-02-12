using System;
using System.Collections.Generic;
using System.Linq;
using PriceBasket.Common.Enum;

namespace PriceBasket.Model.Models
{
    public class Discount : IDiscount
    {
        public Discount()
        {
            DiscountConditions = new List<IDiscountCondition>();
        }

        public DiscountType DiscountType { get; set; }
        public double Value { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<IDiscountCondition> DiscountConditions { get; set; }

        public bool IsValid(IEnumerable<IProduct> basket)
        {
            return DateTime.Today >= Start && DateTime.Today <= End &&
                   CheckBasketConditions(basket);
        }

        private bool CheckBasketConditions(IEnumerable<IProduct> basket)
        {
            var returnVal = true;
            if (DiscountConditions != null)
            {
                Array.ForEach(DiscountConditions.ToArray(), x =>
                {
                    if (returnVal)
                    {
                        returnVal = x.IsValid(basket);
                    }
                });
            }
            return returnVal;
        }

        public string DiscountDetails(IProduct product)
        {
            var returnVal = string.Empty;

            switch (DiscountType)
            {
                case DiscountType.Percentage:
                    product.DiscountValue = (product.Price * product.Quantity) * (Value / 100);
                    returnVal = string.Format("{0} {1}% off: {2:c}", product.Name, Value, 0 - product.DiscountValue);
                    break;
                case DiscountType.FixedValue:
                    product.DiscountValue = Value * product.Quantity;
                    returnVal = string.Format("{0} {1:c} off each: {2:c}", product.Name, Value, 0 - product.DiscountValue);
                    break;
                default:
                    break;
            }

            return returnVal;
        }
    }
}
