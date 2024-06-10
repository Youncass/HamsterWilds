using System;

public class DisposableDelegateAdapter : IDisposable
{
    private readonly Action _toDispose;

    public DisposableDelegateAdapter(Action toDispose) => _toDispose = toDispose;

    public void Dispose() => _toDispose.Invoke();
}
