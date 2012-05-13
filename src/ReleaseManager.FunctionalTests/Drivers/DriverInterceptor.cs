using System;
using System.Collections.Generic;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class DriverInterceptor : Castle.DynamicProxy.IInterceptor
    {
        public int MillisecondsPause { get; set; }
        public bool Log { get; set; }
        private IList<string> _ignoreMethods;

        public DriverInterceptor()
        {
            Log = false;
            MillisecondsPause = 0;
            _ignoreMethods = new List<string> { };
        }

        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            if (!_ignoreMethods.Contains(invocation.Method.Name))
            {
                if (Log) { System.Console.WriteLine("{0}.{1} {2}", invocation.TargetType.Name, invocation.Method.Name, string.Join(" ", invocation.Arguments)); }
            }
            invocation.Proceed();
            if (!_ignoreMethods.Contains(invocation.Method.Name))
            {
                if (MillisecondsPause > 0) { Pause(); }
            }
        }

        public void Pause()
        {
            Console.WriteLine("(wait {0}ms)", MillisecondsPause);
            System.Threading.Thread.Sleep(MillisecondsPause);
        }
    }
}
