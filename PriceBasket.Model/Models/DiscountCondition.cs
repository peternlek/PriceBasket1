using System.Collections.Generic;
using System.Linq;
using PriceBasket.Common.Enum;

namespace PriceBasket.Model.Models
{
    public class DiscountCondition : IDiscountCondition
    {
        public string ProductName { get; set; }
        public DiscountConditionType DiscountConditionType { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// Performs check to see if discount condition is valid based on contents of basket 
        /// </summary>
        /// <param name="basket">Products contained in basket</param>
        /// <returns>Bool indicating validity of condition</returns>
        public bool IsValid(IEnumerable<IProduct> basket)
        {
            var returnVal = false;
            switch (DiscountConditionType)
            {
                case DiscountConditionType.ItemCount:
                    var conditionProduct = basket.FirstOrDefault(x => x.Name == ProductName);
                    returnVal = conditionProduct != null && conditionProduct.Quantity >= Value;
                    break;
                default:
                    break;
            }
            return returnVal;
        }
    }
}
