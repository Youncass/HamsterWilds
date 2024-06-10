using UnityEngine;

namespace Sources.Services.Input
{
    public interface IPlayerInput
    {
        IEvent<Vector2> Movement { get; }
        IEvent<Vector2> CameraRotation { get; }
        IEvent<bool> Jumping { get; }
        IEvent<bool> Sprinting { get; }
        IEvent<bool> Crouching { get; }
        IEvent<bool> Zoom { get; }
    }
}
