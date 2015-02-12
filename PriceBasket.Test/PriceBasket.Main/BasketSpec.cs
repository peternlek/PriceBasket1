using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Common.Enum;
using PriceBasket.Common.Interfaces;
using PriceBasket.DataAccess.Repositories;
using PriceBasket.Model.Models;
using PriceBasket.Test.Helpers;
using Rhino.Mocks;

namespace PriceBasket.Test.PriceBasket.Main
{
    [TestClass]
    public class BasketSpec : BaseTest
    {
        private IProductRepository _productRepository;
        private IBasket _basket;
        private IProduct _testProduct;

        [TestInitialize]
        public void TestInit()
        {
            _productRepository = GenerateStub<IProductRepository>();
            _basket = Container.Resolve<IBasket>();
        }

        [TestMethod]
        public void TestBasketWithNoDiscountsApplied()
        {
            _testProduct = CreateProduct("Soup", 1.00, 1);
            _productRepository.Expect(x => x.GetProducts(new string[] { })).IgnoreArguments().Return(new[] { _testProduct });

            var receiptItems = _basket.GenerateReceipt(new string[] { });

            Assert.AreEqual(2, receiptItems.Length);
            Assert.AreEqual(_testProduct.DiscountValue, 0);
        }

        [TestMethod]
        public void TestPercentageDiscountWhenNoConditionIsApplied()
        {
            _testProduct = CreateProduct("Apples", 1.00, 1);
            AddDiscountToProduct(_testProduct, DiscountType.Percentage, 10);
            _productRepository.Expect(x => x.GetProducts(new string[] {})).IgnoreArguments().Return(new [] { _testProduct });

            var receiptItems = _basket.GenerateReceipt(new string[] { });

            Assert.AreEqual(3, receiptItems.Length);
            Assert.AreEqual(_testProduct.DiscountValue, 0.1);
        }

        [TestMethod]
        public void TestPercentageDiscountWithValidConditionApplied()
        {
            _testProduct = CreateProduct("Bread", 0.80, 1);
            AddDiscountToProduct(_testProduct, DiscountType.Percentage, 50);
            AddDiscountConditionToDiscount(_testProduct.Discounts.First(), "Soup", 2, DiscountConditionType.ItemCount);
            var testProduct2 = CreateProduct("Soup", 0.65, 2);

            _productRepository.Expect(x => x.GetProducts(new string[] { })).IgnoreArguments().Return(new[] { _testProduct, testProduct2 });

            var receiptItems = _basket.GenerateReceipt(new string[] { });

            Assert.AreEqual(3, receiptItems.Length);
            Assert.AreEqual(_testProduct.DiscountValue, 0.4);
        }

        [TestMethod]
        public void TestPercentageDiscountWithInvalidConditionApplied()
        {
            _testProduct = CreateProduct("Bread", 0.80, 1);
            AddDiscountToProduct(_testProduct, DiscountType.Percentage, 50);
            AddDiscountConditionToDiscount(_testProduct.Discounts.First(), "Soup", 2, DiscountConditionType.ItemCount);
            var testProduct2 = CreateProduct("Soup", 0.65, 1);

            _productRepository.Expect(x => x.GetProducts(new string[] { })).IgnoreArguments().Return(new[] { _testProduct, testProduct2 });

            var receiptItems = _basket.GenerateReceipt(new string[] { });

            Assert.AreEqual(2, receiptItems.Length);
            Assert.AreEqual(_testProduct.DiscountValue, 0.0);
        }


        private Product CreateProduct(string name, double price, int quantity)
        {
            return new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
            };
        }

        private void AddDiscountToProduct(IProduct product, DiscountType type, double value)
        {
            product.Discounts.Add(new Discount
            {
                DiscountType = type,
                Start = DateTime.Today,
                End = DateTime.Today.AddDays(1),
                Value = value
            });
        }

        private void AddDiscountConditionToDiscount(IDiscount discount, string productName, int value, DiscountConditionType type)
        {
            discount.DiscountConditions.Add(new DiscountCondition
            {
                ProductName = productName,
                Value = value,
                DiscountConditionType = type
            });
        }
    }
}
