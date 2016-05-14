using System;
using Data;
using NLog;

namespace Client
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #region Entry point

        private static Program _program;

        static void Main(string[] args)
        {
            _program = new Program();
            _program.Run(args);
        }

        private void Run(string[] args)
        {
            var client = new MonitoringClient("http://127.0.0.1:8085/monitoring/");
            var container = client.GetLocationStates();
            Display(container);

            Console.ReadKey();
        }

        private void Display(LocationStateContainer contaner)
        {
            foreach (LocationState state in contaner.States)
            {
                Console.WriteLine("Id = {1}{0}MainEntranceClosed = {2}{0}WindowClosed = {3}",
                    Environment.NewLine, state.Id, state.MainEntranceClosed, state.WindowClosed);
            }
            Console.WriteLine("------------------------------------------");
        }

        #endregion
    }
}
