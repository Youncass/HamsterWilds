using UnityEngine;

namespace Sources.Services.AssetManagement
{
    public interface IAssets
    {
        T Load<T>(string path) where T : Object;
    }
}
