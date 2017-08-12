using System.Threading;

namespace TextLoggerNet.Helpers
{
    public interface IEventWaitHandleWrapper
    {
        bool WaitOne(int i, bool b);
        void Set();
    }

    public class EventWaitHandleWrapper : IEventWaitHandleWrapper
    {
        readonly EventWaitHandle _handle;
        public EventWaitHandleWrapper(EventWaitHandle eventWaitHandle)
        {
            _handle = eventWaitHandle;
        }

        public bool WaitOne(int i, bool b) => _handle.WaitOne(i, b);
        public void Set() => _handle.Set();
    }

    public interface IEventWaitHandleWrapperProvider
    {
        IEventWaitHandleWrapper New(bool b, EventResetMode autoReset, string name);
    }

    public class EventWaitHandleWrapperProvider : IEventWaitHandleWrapperProvider
    {
        public IEventWaitHandleWrapper New(bool b, EventResetMode autoReset, string name)
            => new EventWaitHandleWrapper(new EventWaitHandle(b, autoReset, name));
    }
}
