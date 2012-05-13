using System;
using WatiN.Core;
using Castle.DynamicProxy;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class TestDriver: IDisposable
    {
        private readonly ProxyGenerator _proxyGenerator;
        private readonly DriverInterceptor _interceptor;

        public TimeSpan Pause 
        {
            get
            {
                return TimeSpan.FromMilliseconds(_interceptor.MillisecondsPause);
            }
            set
            {
                _interceptor.MillisecondsPause = (int)value.TotalMilliseconds;
            }
        }

        public bool Log
        {
            get
            {
                return _interceptor.Log;
            }
            set
            {
                _interceptor.Log = value;
            }
        }

        public TestDriver(Browser browser, bool log, TimeSpan pause)
        {
            Browser = browser;
            _proxyGenerator = new ProxyGenerator();
            _interceptor = new DriverInterceptor();
            Log = log;
            Pause = pause;
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
