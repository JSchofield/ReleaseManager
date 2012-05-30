using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using WatiN.Core;
using ReleaseManager.FunctionalTests.WebServer;
using ReleaseManager.FunctionalTests.Drivers;
using ReleaseManager.Tests.Persistence;

namespace ReleaseManager.FunctionalTests.Tests
{
    public class DevServerTestBase
    {
        private const string Port = "57228"; // Matches the DevelopmentServerPort in ReleaseManager.Web

        private readonly WebServerProcess _webServer;
        protected bool LogDriverCommands;
        protected TimeSpan PauseBeforeCommand;

        public TestDriver Driver { get; private set; }

        public DevServerTestBase(): this(false, TimeSpan.Zero)
        {
        }

        public DevServerTestBase(bool logDriverCommands, TimeSpan pauseBeforeCommand)
        {
            LogDriverCommands = logDriverCommands;
            PauseBeforeCommand = pauseBeforeCommand;
            _webServer = CreateWebServerProcess();
        }

        private WebServerProcess CreateWebServerProcess()
        {
            var testAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", string.Empty);
            var exePath = Path.Combine(testAssemblyLocation, @"WebServer\WebDev.WebServer40.EXE");
            var websitePath = Path.Combine(Path.GetDirectoryName(testAssemblyLocation), "ReleaseManager.Web");

            Console.WriteLine("Configuring development web server:\n\tExe:\t{0}\n\tSite:\t{2}\n\tPort:\t{1}", exePath, Port, websitePath);
            return new WebServerProcess(exePath, websitePath, Port);
        }

        [SetUp]
        public void SetUp()
        {
            SQLiteDatabase.CreateInMemoryRepository();
            StartWebServer(_webServer);
            Driver = CreateDriver(_webServer.UrlStem.ToString(), LogDriverCommands, PauseBeforeCommand);
        }
        
        [TearDown]
        public void TearDown()
        {
            TearDownDriver(Driver);
            StopWebServer(_webServer);
        }

        private void StartWebServer(WebServerProcess webServer)
        {
            Console.WriteLine("Starting development web server");
            webServer.Start();
        }

        private void StopWebServer(WebServerProcess webServer)
        {
            Console.WriteLine("Stopping development web server");
            webServer.Stop();
        }

        private TestDriver CreateDriver(string url, bool logDriverCommands, TimeSpan pauseBeforeCommand)
        {
            return new TestDriver(new IE(url), logDriverCommands, pauseBeforeCommand);
        }

        private void TearDownDriver(TestDriver driver)
        {
            if (driver != null)
            {
                driver.Dispose();
            }
        }
    }
}