using UnityEngine;

namespace Sources.Services.PlayerFactory
{
    public interface IPlayerFactory
    {
        void CreatePlayer(Vector3 at);
        void DestroyPlayer();
    }
}
