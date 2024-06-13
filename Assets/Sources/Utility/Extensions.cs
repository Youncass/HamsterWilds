using System;
using UnityEngine;
using Zenject;

namespace Sources
{
    public static class Extensions
    {
        public static T GetComponentChecked<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() ?? throw new NullReferenceException(typeof(T).FullName);
        }

        public static T New<T>(this DiContainer container)
        {
            container.Bind<T>().FromNew().AsCached().NonLazy();
            return container.Resolve<T>();
        }
    }
}
