using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Rhino.Mocks;

namespace PriceBasket.Test.Helpers
{
    public class BaseTest
    {
        /// <summary>
        /// The unity container that view models use etc.
        /// </summary>
        protected IUnityContainer Container { private set; get; }

        /// <summary>
        /// Base test class contructor
        /// </summary>
        public BaseTest()
        {
            Container = new UnityContainer().LoadConfiguration();
            Container.RegisterInstance(typeof (IUnityContainer), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Generates a stubbed object and registers it with Unity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GenerateStub<T>() where T : class
        {
            var stub = MockRepository.GenerateStub<T>();
            Container.RegisterInstance<T>(stub);
            return stub;
        }

        protected T Resolve<T>(params ResolverOverride[] resolverOverrides)
        {
            return Container.Resolve<T>(resolverOverrides);
        }
    }
}
