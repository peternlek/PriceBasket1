using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using PriceBasket.Common.Interfaces;
using PriceBasket.DataAccess.Repositories;

namespace PriceBasket.Main
{
    /// <summary>
    /// Respinsible for productng a receipt for products and their discounts
    /// </summary>
    public class Basket : IBasket
    {
        [Dependency] public IProductRepository ProductRepository { get; set; }

        /// <summary>
        /// Generate output of receipt
        /// </summary>
        public string[] GenerateReceipt(string[] stringProducts)
        {
            var receiptItems = new List<string>();
            
            var products = ProductRepository.GetProducts(stringProducts);

            receiptItems.Add(string.Format("SubTotal: {0:c}", products.Sum(x => x.Price * x.Quantity)));
            Array.ForEach(products.SelectMany(x => x.CheckForDiscounts(products)).Where(x => !string.IsNullOrEmpty(x)).ToArray(), receiptItems.Add);
            receiptItems.Add(string.Format("Total: {0:c}", products.Sum(x => x.Price * x.Quantity) - products.Sum(x => x.DiscountValue)));

            return receiptItems.ToArray();
        }
    }
}
