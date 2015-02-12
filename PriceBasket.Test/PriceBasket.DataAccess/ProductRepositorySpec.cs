using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Common;
using PriceBasket.DataAccess.Repositories;
using PriceBasket.Test.Helpers;

namespace PriceBasket.Test.PriceBasket.DataAccess
{
    [TestClass]
    public class ProductRepositorySpec : BaseTest
    {
        private ProductRepository _productRepository;

        [TestInitialize]
        public void TestInit()
        {
            _productRepository = new ProductRepository();
        }

        [TestMethod]
        public void TestValidProductsAreLoadedSuccessfully()
        {
            var products = _productRepository.GetProducts(new[] { "Soup", "Milk" });

            Assert.AreEqual(2, products.Length);
        }

        [TestMethod]
        public void TestInvalidProductsAreFlaggedSuccessfully()
        {
            var products = _productRepository.GetProducts(new[] { "abc", "def", "Soup" });

            Assert.AreEqual(3, products.Length);
            Assert.AreEqual(2, products.Count(x => x.Name.StartsWith(Constants.UnknownProduct.Substring(0, 11))));
            Assert.AreEqual(1, products.Count(x => !x.Name.StartsWith(Constants.UnknownProduct.Substring(0, 11))));
        }
    }
}
