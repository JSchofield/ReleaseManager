using System;
using WatiN.Core;
using Castle.DynamicProxy;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class TestDriver: IDisposable
    {
        private readonly ProxyGenerator _proxyGenerator;
        private readonly IInterceptor _interceptor;

        public TestDriver(Browser browser, bool log, TimeSpan pause)
        {
            Browser = browser;
            _interceptor = new DriverInterceptor(log, pause);
            _proxyGenerator =  new ProxyGenerator();
        }

        public Browser Browser { get; private set; }

        public Home HomePage()
        {
            return CreatePageDriver<Home>();
        }

        public T CreatePageDriver<T>() where T : WatinPageDriver
        {
            try
            {
                return (T)_proxyGenerator.CreateClassProxy(typeof(T), new[] { this }, _interceptor);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not create class proxy for {0}: {1}", typeof(T).Name, e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            if (Browser != null)
            {
                Browser.Dispose();
            }
        }
    }
}
