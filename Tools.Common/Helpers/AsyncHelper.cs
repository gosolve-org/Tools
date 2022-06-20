namespace GoSolve.Tools.Common.Helpers;

/// <summary>
/// Helper class for asynchronous functionality.
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    /// Get 2 results in parallel.
    /// </summary>
    /// <typeparam name="T1">Type of task 1.</typeparam>
    /// <typeparam name="T2">Type of task 2.</typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static async Task<(T1, T2)> GetInParallel<T1, T2>(Task<T1> t1, Task<T2> t2)
    {
        await Task.WhenAll(t1, t2);

        return (t1.Result, t2.Result);
    }

    /// <summary>
    /// Get 3 results in parallel.
    /// </summary>
    /// <typeparam name="T1">Type of task 1.</typeparam>
    /// <typeparam name="T2">Type of task 2.</typeparam>
    /// <typeparam name="T3">Type of task 3.</typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <returns></returns>
    public static async Task<(T1, T2, T3)> GetInParallel<T1, T2, T3>(Task<T1> t1, Task<T2> t2, Task<T3> t3)
    {
        await Task.WhenAll(t1, t2, t3);

        return (t1.Result, t2.Result, t3.Result);
    }

    /// <summary>
    /// Get 4 results in parallel.
    /// </summary>
    /// <typeparam name="T1">Type of task 1.</typeparam>
    /// <typeparam name="T2">Type of task 2.</typeparam>
    /// <typeparam name="T3">Type of task 3.</typeparam>
    /// <typeparam name="T4">Type of task 4.</typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <param name="t4"></param>
    /// <returns></returns>
    public static async Task<(T1, T2, T3, T4)> GetInParallel<T1, T2, T3, T4>(
        Task<T1> t1, Task<T2> t2, Task<T3> t3, Task<T4> t4)
    {
        await Task.WhenAll(t1, t2, t3, t4);

        return (t1.Result, t2.Result, t3.Result, t4.Result);
    }

    /// <summary>
    /// Get 5 results in parallel.
    /// </summary>
    /// <typeparam name="T1">Type of task 1.</typeparam>
    /// <typeparam name="T2">Type of task 2.</typeparam>
    /// <typeparam name="T3">Type of task 3.</typeparam>
    /// <typeparam name="T4">Type of task 4.</typeparam>
    /// <typeparam name="T5">Type of task 5.</typeparam>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    /// <param name="t4"></param>
    /// <param name="t5"></param>
    /// <returns></returns>
    public static async Task<(T1, T2, T3, T4, T5)> GetInParallel<T1, T2, T3, T4, T5>(
        Task<T1> t1, Task<T2> t2, Task<T3> t3, Task<T4> t4, Task<T5> t5)
    {
        await Task.WhenAll(t1, t2, t3, t4, t5);

        return (t1.Result, t2.Result, t3.Result, t4.Result, t5.Result);
    }
}
