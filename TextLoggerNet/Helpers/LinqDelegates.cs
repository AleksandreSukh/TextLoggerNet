namespace TextLoggerNet.Helpers
{
    //Func
    //public delegate TResult Trunc<out TResult>();
    //public delegate TResult Trunc<in T, out TResult>(T a);

    public delegate TResult Trunc<TResult>();
    public delegate TResult Trunc<T, TResult>(T a);
    public delegate TResult Trunc<T1, T2, TResult>(T1 arg1, T2 arg2);
    public delegate TResult Trunc<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult Trunc<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    //Action

    public delegate void Fiction();
    public delegate void Fiction<T1>(T1 arg1);
    public delegate void Fiction<T1, T2>(T1 arg1, T2 arg2);
    public delegate void Fiction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate void Fiction<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);


}