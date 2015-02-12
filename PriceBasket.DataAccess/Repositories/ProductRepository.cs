using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using PriceBasket.Common;
using PriceBasket.Common.Enum;
using PriceBasket.Model.Models;

namespace PriceBasket.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Product data store
        /// </summary>
        private readonly IList<IProduct> _allProducts = new List<IProduct>();

        /// <summary>
        /// Product repository constructor
        /// </summary>
        public ProductRepository()
        {
            var doc = new XmlDocument();
            doc.Load(@"Data\Products.xml");

            var root = doc.DocumentElement;

            if (root != null)
            {
                var products = root.SelectNodes("product");
                if (products != null)
                {
                    GenerateProducts(products);
                }
            }
        }

        /// <summary>
        /// Generate products from xml store
        /// </summary>
        /// <param name="products">Product nodes</param>
        private void GenerateProducts(XmlNodeList products)
        {
            foreach (XmlNode xmlProduct in products)
            {
                var product = new Product
                {
                    Name = xmlProduct.Attributes["name"].Value,
                    Price = double.Parse(xmlProduct.Attributes["price"].Value),
                };
                GenerateDiscounts(xmlProduct, product);
                _allProducts.Add(product);
            }
        }

        /// <summary>
        /// Generate Discounts from xml store
        /// </summary>
        /// <param name="xmlProduct">Product xml node</param>
        /// <param name="product">Product object</param>
        private void GenerateDiscounts(XmlNode xmlProduct, Product product)
        {
            var discounts = xmlProduct.SelectNodes("discounts/discount");
            if (discounts != null)
            {
                foreach (XmlNode xmlDiscount in discounts)
                {
                    var discount = new Discount();
                    discount.DiscountType =
                        (DiscountType)Enum.Parse(typeof(DiscountType), xmlDiscount.Attributes["type"].Value);
                    discount.End = DateTime.Parse(xmlDiscount.Attributes["end"].Value);
                    discount.Start = DateTime.Parse(xmlDiscount.Attributes["start"].Value);
                    discount.Value = double.Parse(xmlDiscount.Attributes["value"].Value);
                    GenerateDiscountConditions(xmlDiscount, discount);
                    product.Discounts.Add(discount);
                }
            }
        }

        /// <summary>
        /// Generate Discounts from xml store
        /// </summary>
        /// <param name="xmlDiscount">Discount xml node</param>
        /// <param name="discount">Discount object</param>
        private void GenerateDiscountConditions(XmlNode xmlDiscount, Discount discount)
        {
            var discountConditionss = xmlDiscount.SelectNodes("discountConditions/discountCondition");
            if (discountConditionss != null)
            {
                foreach (XmlNode xmlDiscountCondition in discountConditionss)
                {
                    var discountCondition = new DiscountCondition
                    {
                        DiscountConditionType = (DiscountConditionType)Enum.Parse(typeof(DiscountConditionType), xmlDiscountCondition.Attributes["type"].Value),
                        Value = int.Parse(xmlDiscountCondition.Attributes["value"].Value),
                        ProductName = xmlDiscountCondition.Attributes["productName"].Value
                    };
                    discount.DiscountConditions.Add(discountCondition);
                }
            }
        }

        /// <summary>
        /// Generate concrete products from string values, flag any invalid products
        /// </summary>
        /// <param name="stringProducts">Array of product string values</param>
        /// <returns>Array of Products</returns>
        public IProduct[] GetProducts(string[] stringProducts)
        {
            return stringProducts.GroupBy(x => x).Select(x =>
            {
                var product = _allProducts.Any(y => y.Name == x.Key)
                    ? _allProducts.First(y => y.Name == x.Key)
                    : new Product { Name = string.Format(Constants.UnknownProduct, x.Key) };
                product.Quantity = x.Count();
                return product;
            }).ToArray();
        }
    }
}
