using System;
using System.Collections.Generic;
using System.Text;
using TextLoggerNet.Interfaces;

namespace TextLoggerNet.LoggingTools
{
    public class RunningMethodLogger : IDisposable, IRunningMethodLogger
    {
        private readonly ITextLogger _textLogger;
        private readonly System.Timers.Timer _timer;
        string _currentActivityName = "start";
        int _internalCounter;

        public RunningMethodLogger(ITextLogger textLogger, TimeSpan updateInterval)
        {
            _textLogger = textLogger;
            _timer = new System.Timers.Timer(updateInterval.TotalMilliseconds);
            _timer.Elapsed +=
                (s, e) => { textLogger.WriteLine($"\t\t\t{TimeSpan.FromSeconds(++_internalCounter * updateInterval.TotalSeconds).ToVerboseStingHMS()} Since {_currentActivityName}"); };
        }

        public void ResetAction(string newName)
        {
            _currentActivityName = newName;
            _textLogger.WriteLine("===> " + _currentActivityName);
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
