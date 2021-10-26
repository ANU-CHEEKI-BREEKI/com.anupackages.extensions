using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExtensions
{
    /// <summary>
    /// Start a coroutine that might throw an exception. Call the callback with the exception if it
    /// does or null if it finishes without throwing an exception.
    /// </summary>
    public static Coroutine StartThrowingCoroutine(this MonoBehaviour monoBehaviour, IEnumerator enumerator, Action onSuccess, Action<Exception> onException)
        => monoBehaviour.StartCoroutine(enumerator.RunThrowingIterator(onSuccess, onException));

    /// <summary>
    /// Run an iterator function that might throw an exception. Call the callback with the exception
    /// if it does or null if it finishes without throwing an exception.
    /// </summary>
    public static IEnumerator RunThrowingIterator(this IEnumerator enumerator, Action onSuccess, Action<Exception> onException)
    {
        while (true)
        {
            object current;
            try
            {
                if (!enumerator.MoveNext())
                    break;
                current = enumerator.Current;
            }
            catch (Exception ex)
            {
                onException?.Invoke(ex);
                yield break;
            }
            yield return current;
        }
        onSuccess?.Invoke();
    }

    public static Coroutine InvokeDelayed(this MonoBehaviour monoBehaviour, Action method, float delay)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutine(method, delay));
    }

    public static Coroutine InvokeDelayedUnscaled(this MonoBehaviour monoBehaviour, Action method, float delay)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineUnscaled(method, delay));
    }

    public static Coroutine InvokeDelayedRepeating(this MonoBehaviour monoBehaviour, Action method, float delay, float repeatRate)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineRepeated(method, delay, repeatRate));
    }

    public static Coroutine InvokeDelayedRepeatingUnscaled(this MonoBehaviour monoBehaviour, Action method, float delay, float repeatRate)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineRepeatedUnscaled(method, delay, repeatRate));
    }

    /// <summary>
    /// new WaitForEndOfFrame(); вызывется только если активно окно Game!!! аккуратно при дебаге!
    /// </summary>
    public static Coroutine InvokeWaitEndOfFrame(this MonoBehaviour monoBehaviour, Action method)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineWaitFrame(method));
    }

    public static Coroutine InvokeWaitForFixedUpdate(this MonoBehaviour monoBehaviour, Action method)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineWaitForFixedUpdate(method));
    }

    public static Coroutine InvokeSkipOneFrame(this MonoBehaviour monoBehaviour, Action method)
        => InvokeSkipFrames(monoBehaviour, method, 1);

    public static Coroutine InvokeSkipFrames(this MonoBehaviour monoBehaviour, Action method, int framesCount)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineSkipFrames(method, framesCount));
    }

    public static Coroutine InvokeSkipFixedUpdateFrames(this MonoBehaviour monoBehaviour, Action method, int framesCount)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;

        return monoBehaviour.StartCoroutine(InvokeCoroutineSkipFixedFrames(method, framesCount));
    }

    /// <summary>
    /// </summary>
    /// <param name="monoBehaviour"></param>
    /// <param name="duration"> if <= 0 - calls looped</param>
    /// <param name="deltaTimeScalerProvider">can not be nul. if returns < 0 - completed</param>
    /// <param name="completed"></param>
    /// <returns></returns>
    public static Coroutine InvokeDelayedRepeatingScaled(this MonoBehaviour monoBehaviour, float duration, Func<float> deltaTimeScalerProvider, Action completed)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy)
            return null;
        return monoBehaviour.StartCoroutine(InvokeCoroutineRepeatedScaled(duration, deltaTimeScalerProvider, completed));
    }

    /// <summary>
    ///  Suspends the coroutine execution until the supplied delegate evaluates to true.
    /// </summary>
    public static Coroutine InvokeWaitUntil(this MonoBehaviour monoBehaviour, Action method, Func<bool> predicate)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy || method == null || predicate == null)
            return null;
        return monoBehaviour.StartCoroutine(CoroutineWaitUntil(method, predicate));
    }
    /// <summary>
    /// Suspends the coroutine execution until the supplied delegate evaluates to false.
    /// </summary>
    public static Coroutine InvokeWaitWhile(this MonoBehaviour monoBehaviour, Action method, Func<bool> predicate)
    {
        if (monoBehaviour == null || !monoBehaviour.gameObject.activeInHierarchy || method == null || predicate == null)
            return null;
        return monoBehaviour.StartCoroutine(CoroutineWaitWhile(method, predicate));
    }

    private static IEnumerator InvokeCoroutineUnscaled(Action method, float delay)
    {
        if (method == null)
            yield break;

        yield return new WaitForSecondsRealtime(delay);
        method?.Invoke();
    }

    private static IEnumerator InvokeCoroutineRepeated(Action method, float delay, float repeatRate)
    {
        if (method == null)
            yield break;

        yield return new WaitForSeconds(delay);

        var secondsWaiter = new WaitForSeconds(repeatRate);
        while (true)
        {
            method?.Invoke();
            yield return secondsWaiter;
        }
    }

    private static IEnumerator InvokeCoroutine(Action method, float delay)
    {
        if (method == null)
            yield break;

        yield return new WaitForSeconds(delay);

        method?.Invoke();
    }

    private static IEnumerator InvokeCoroutineRepeatedUnscaled(Action method, float delay, float repeatRate)
    {
        if (method == null)
            yield break;

        yield return new WaitForSecondsRealtime(delay);

        var secondsWaiter = new WaitForSecondsRealtime(repeatRate);
        while (true)
        {
            method?.Invoke();
            yield return secondsWaiter;
        }
    }

    private static readonly WaitForEndOfFrame _endOfFrameWaiter = new WaitForEndOfFrame();
    private static IEnumerator InvokeCoroutineWaitFrame(Action method)
    {
        yield return _endOfFrameWaiter;
        method?.Invoke();
    }

    private static readonly WaitForFixedUpdate _fixedUpdateWaiter = new WaitForFixedUpdate();
    private static IEnumerator InvokeCoroutineWaitForFixedUpdate(Action method)
    {
        yield return _fixedUpdateWaiter;
        method?.Invoke();
    }

    private static IEnumerator InvokeCoroutineSkipFrames(Action method, int framesCount)
    {
        if (method == null)
            yield break;

        for (int i = 0; i < framesCount; i++)
            yield return null;
        method?.Invoke();
    }

    private static IEnumerator InvokeCoroutineSkipFixedFrames(Action method, int framesCount)
    {
        if (method == null)
            yield break;

        for (int i = 0; i < framesCount; i++)
            yield return _fixedUpdateWaiter;
        method?.Invoke();
    }

    private static IEnumerator InvokeCoroutineRepeatedScaled(float duration, Func<float> deltaTimeScalerProvider, Action completed)
    {
        if (deltaTimeScalerProvider == null)
            yield break;

        var elapced = duration;
        while (duration <= 0 || elapced > 0)
        {
            var scaler = deltaTimeScalerProvider.Invoke();
            if (scaler < 0)
                break;

            elapced -= Time.deltaTime * scaler;
            yield return null;
        }

        completed?.Invoke();
    }

    private static IEnumerator CoroutineWaitUntil(Action method, Func<bool> predicate)
    {
        yield return new WaitUntil(predicate);
        method?.Invoke();
    }

    private static IEnumerator CoroutineWaitWhile(Action method, Func<bool> predicate)
    {
        yield return new WaitWhile(predicate);
        method?.Invoke();
    }
}