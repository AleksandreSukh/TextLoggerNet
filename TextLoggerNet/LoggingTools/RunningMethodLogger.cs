using System;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.LoggingTools
{
    public class RunningMethodLogger : IDisposable, IRunningMethodLogger
    {
        private readonly ILogger _logger;
        private readonly System.Timers.Timer _timer;
        string _currentActivityName = "start";
        int _internalCounter;

        public RunningMethodLogger(ILogger logger, TimeSpan updateInterval)
        {
            _logger = logger;
            _timer = new System.Timers.Timer(updateInterval.TotalMilliseconds);
            _timer.Elapsed +=
                (s, e) => { logger.WriteLine($"\t\t\t{TimeSpan.FromSeconds(++_internalCounter * updateInterval.TotalSeconds).ToVerboseStingHMS()} Since {_currentActivityName}"); };
        }

        public void ResetAction(string newName)
        {
            _currentActivityName = newName;
            _logger.WriteLine("===> " + _currentActivityName);
            _internalCounter = 0;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SetActionName(string message) => _currentActivityName = message;

        public void Start() => _timer.Start();
    }

}
