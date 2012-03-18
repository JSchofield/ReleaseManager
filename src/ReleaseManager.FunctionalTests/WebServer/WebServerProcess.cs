using System;
using System.Diagnostics;
using ReleaseManager.FunctionalTests.Drivers;

namespace ReleaseManager.FunctionalTests.WebServer
{
    public class WebServerProcess: IDisposable
    {
        private readonly Process _process;

        public WebServerProcess(string serverExe, string path, string port)
        {
            _process = CreateProcess(serverExe, string.Format("/path:\"{0}\" /port:{1}", path, port), ".");
            UrlStem = new Uri(string.Format("http://localhost:{0}/", port));
        }

        public Uri UrlStem { get; private set; }

        public void Start()
        {
            _process.Start();
        }

        public void Stop()
        {
            _process.Kill();
        }

        private Process CreateProcess(string fileName, string arguments, string workingDir)
        {
            Process process =
                new Process() {
                    StartInfo = new ProcessStartInfo{
                        FileName = fileName,
                        Arguments = arguments,
                        WorkingDirectory = workingDir,
                        RedirectStandardError = false,
                        RedirectStandardOutput = false,
                        UseShellExecute = true } };

            return process;
        }

        public void Dispose()
        {
            if (_process != null)
            {
                _process.Dispose();
            }
        }
    }
}
