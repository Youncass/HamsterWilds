using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

namespace Sources.Services.Input
{
    public class PlayerInput : IPlayerInput, IDisposable
    {
        private readonly Input<Vector2> _movement;
        private readonly Input<Vector2> _cameraRotation;
        private readonly Input<bool> _jumping;
        private readonly Input<bool> _sprinting;
        private readonly Input<bool> _crouching;
        private readonly Input<bool> _zoom;

        private readonly CompositeDisposable _subscriptions = new();

        private const string ActionMap = "Player";

        private const string MovementAction = "Movement";
        private const string CameraRotationAction = "CameraRotation";
        private const string JumpingAction = "Jumping";
        private const string SprintingAction = "Sprinting";
        private const string CrouchingAction = "Crouching";
        private const string ZoomAction = "Zoom";

        public IEvent<Vector2> Movement => _movement;
        public IEvent<Vector2> CameraRotation => _cameraRotation;
        public IEvent<bool> Jumping => _jumping;
        public IEvent<bool> Sprinting => _sprinting;
        public IEvent<bool> Crouching => _crouching;
        public IEvent<bool> Zoom => _zoom;
        
        public PlayerInput(InputActionAsset playerControls)
        {
            InputActionMap actionMap = playerControls.FindActionMap(ActionMap, throwIfNotFound: true);

            _movement = new Input<Vector2>(actionMap.FindAction(MovementAction))
                .AddTo(_subscriptions);
            _cameraRotation = new Input<Vector2>(actionMap.FindAction(CameraRotationAction))
                .AddTo(_subscriptions);

            _jumping = CreateKeyInput(JumpingAction);
            _sprinting = CreateKeyInput(SprintingAction);
            _crouching = CreateKeyInput(CrouchingAction);
            _zoom = CreateKeyInput(ZoomAction);



            Input<bool> CreateKeyInput(string actionName)
                => new Input<bool>(actionMap.FindAction(actionName),
                    performProcessor: context => context.ReadValue<float>() == 1,
                    cancelProcessor: context => context.ReadValue<float>() == 1)
                    .AddTo(_subscriptions);
        }

        public void Dispose()
        {
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();
        }
    }
}
