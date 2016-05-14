using System;
using Nancy.Hosting.Self;
using NLog;

namespace Server
{
    class Program
    {
        #region Entry point

        private static Program _program;

        static void Main(string[] args)
        {
            _program = new Program();
            _program.Run(args);
        }

        #endregion

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        private void Run(string[] args)
        {
            Uri uri = new Uri("http://127.0.0.1:8085/monitoring/");
            using (var host = new NancyHost(uri))
            {
                try
                {
                    host.Start();

                    _logger.Info("Application started.");
                    _logger.Info("Address: " + uri);
                }
                catch (Exception ex)
                {
                    _logger.Error("{1}{0}{2}", Environment.NewLine, ex.Message, ex.StackTrace);
                }

                Console.ReadKey();
            }
        }
    }
}
