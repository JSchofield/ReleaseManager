using System;
using System.Collections.Generic;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class DriverInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly int _millisecondsPause;
        private readonly bool _log;
        private IList<string> _ignoreMethods;

        public DriverInterceptor(bool log, TimeSpan pause)
        {
            _log = log;
            _millisecondsPause = (int)pause.TotalMilliseconds;
            _ignoreMethods = new List<string> { };
        }

        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            if (!_ignoreMethods.Contains(invocation.Method.Name))
            {
                if (_log) { System.Console.WriteLine("{0}.{1} {2}", invocation.TargetType.Name, invocation.Method.Name, string.Join(" ", invocation.Arguments)); }
            }
            invocation.Proceed();
            if (!_ignoreMethods.Contains(invocation.Method.Name))
            {
                if (_millisecondsPause > 0) { Pause(); }
            }
        }

        public void Pause()
        {
            Console.WriteLine("(wait {0}ms)", _millisecondsPause);
            System.Threading.Thread.Sleep(_millisecondsPause);
        }
    }
}
