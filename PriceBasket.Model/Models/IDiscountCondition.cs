using System.Collections.Generic;
using PriceBasket.Common.Enum;

namespace PriceBasket.Model.Models
{
    public interface IDiscountCondition
    {
        string ProductName { get; set; }
        DiscountConditionType DiscountConditionType { get; set; }
        int Value { get; set; }
        bool IsValid(IEnumerable<IProduct> basket);
    }
}