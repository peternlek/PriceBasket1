using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceBasket.Common.Interfaces;
using PriceBasket.Test.Helpers;
using Rhino.Mocks;

namespace PriceBasket.Test.PriceBasket.ToConsole
{
    [TestClass]
    public class BasketSpec : BaseTest
    {
        private IGenerateReceipt _receiptGenerator;
        private IBasket _basket;

        [TestInitialize]
        public void TestInit()
        {
            _receiptGenerator = GenerateStub<IGenerateReceipt>();
            _basket = Container.Resolve<IBasket>();
        }

        [TestMethod]
        public void TestReceiptGeneratorIsCalled()
        {
            _receiptGenerator.Expect(x => x.ProduceReceipt(new string[] {})).IgnoreArguments();

            _basket.GenerateReceipt(new string[] { });
        }
    }
}
