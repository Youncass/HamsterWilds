using System.Collections.Generic;
using UnityEngine;

namespace Sources.Services.AssetManagement
{
    public class Assets : IAssets
    {
        private readonly Dictionary<string, Object> _cache = new();

        public T Load<T>(string path)
            where T : Object
        {
            if (_cache.ContainsKey(path))
                return (T)_cache[path];

            var resource = Resources.Load<T>(path);
            _cache[path] = resource;
            return resource;
        }
    }
}
