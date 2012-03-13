using System;
using System.Collections.Generic;
using System.Linq;
using WatiN.Core;
using Castle.DynamicProxy;

namespace ReleaseManager.FunctionalTests.Driver
{
    public class TestDriver
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
            return Proxy<Home>();
        }

        public ReleaseList ReleaseList()
        {
            return Proxy<ReleaseList>();
        }

        public ComponentList ComponentList()
        {
            return Proxy<ComponentList>();
        }

        private T Proxy<T>() where T : WatinPageDriver
        {
            return (T)_proxyGenerator.CreateClassProxy(typeof(T), new []{ this }, _interceptor);
        }
    }
}
