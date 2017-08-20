namespace TextLoggerNet.LoggingTools
{
    public interface IRunningMethodLogger
    {
        void Dispose();
        void ResetAction(string newName);
        void SetActionName(string message);
        void Start();
    }
}