using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PriceBasket.Common.Interfaces;

namespace PriceBasket.Core
{
    public class Bootstrap : IBootstrap
    {
        public static IUnityContainer Container { get; set; }

        public Bootstrap()
        {
            Container = new UnityContainer().LoadConfiguration();
            Container.RegisterInstance(typeof (IUnityContainer), new ContainerControlledLifetimeManager());
        }

        public void Run(string[] args)
        {
            using (Container)
            {
                Container.Resolve<IGenerateReceipt>().ProduceReceipt(args);
            }
        }
    }
}
