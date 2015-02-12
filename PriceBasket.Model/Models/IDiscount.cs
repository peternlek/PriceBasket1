using System;
using System.Collections.Generic;
using PriceBasket.Common.Enum;

namespace PriceBasket.Model.Models
{
    public interface IDiscount
    {
        DiscountType DiscountType { get; set; }
        double Value { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        IList<IDiscountCondition> DiscountConditions { get; set; }
        bool IsValid(IEnumerable<IProduct> basket);
        string DiscountDetails(IProduct product);
    }
}