using System;
using UnityEngine.InputSystem;
using UniRx;

namespace Sources.Services.Input
{
    public class Input<T> : IEvent<T>, IDisposable
        where T : struct
    {
        public bool HasValue { get; private set; } = false;
        public T Value { get; private set; }

        private readonly Subject<T> _subject;
        private readonly InputAction _action;

        private readonly Func<InputAction.CallbackContext, T> _performProcessor;
        private readonly Func<InputAction.CallbackContext, T> _cancelProcessor;

        public Input(InputAction action,
            Func<InputAction.CallbackContext, T> performProcessor = null,
            Func<InputAction.CallbackContext, T> cancelProcessor = null)
        {
            _action = action;
            _performProcessor = performProcessor ?? (context => context.ReadValue<T>());
            _cancelProcessor = cancelProcessor ?? (context => context.ReadValue<T>());

            _subject = new Subject<T>();

            _action.performed += OnPerform;
            _action.canceled += OnCancel;

            _action.Enable();
        }

        private void OnPerform(InputAction.CallbackContext context)
        {
            var newValue = _performProcessor(context);
            _subject.OnNext(newValue);
            Value = newValue;
            HasValue = true;
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            var newValue = _cancelProcessor(context);
            _subject.OnNext(newValue);
            Value = newValue;
        }

        public void Dispose()
        {
            _subject.OnCompleted();
            _subject.Dispose();

            _action.Disable();

            _action.performed -= OnPerform;
            _action.canceled -= OnCancel;
        }

        public IDisposable Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
    }
}
