using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Common.Interfaces;
using PriceBasket.Core;
using PriceBasket.Test.Helpers;
using Rhino.Mocks;

namespace PriceBasket.Test.PriceBasket.Core
{
    [TestClass]
    public class BootstrapSpec : BaseTest
    {
        private Bootstrap _bootStrap;

        [TestInitialize]
        public void TestInit()
        {
            _bootStrap = new Bootstrap();
        }

        [TestMethod]
        public void TestContainerIsCreatedSuccessfully()
        {
            Assert.IsNotNull(Bootstrap.Container);
        }

        [TestMethod]
        public void TestBasketIsAvailableInContainer()
        {
            var basket = GenerateStub<IGenerateReceipt>();
            basket.Expect(x => x.ProduceReceipt(new string[] {})).IgnoreArguments();
            Bootstrap.Container = Container;

            _bootStrap.Run(new string[] { });
        }
    }
}
