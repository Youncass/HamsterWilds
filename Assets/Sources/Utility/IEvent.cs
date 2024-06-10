using System;

namespace Sources.Services.Input
{
    public interface IEvent<T> : IObservable<T>
    {
        bool HasValue { get; }
        T Value { get; }
    }
}
