using System;
using UnityEngine;

namespace Sources.GamePlay.Interaction
{
    public record InteractionObject(GameObject GameObject) : IHoverable, IStartDraggable, IDraggable, IStopDraggable, IUsable
    {
        public IHoverable[] Hoverables => GameObject.GetComponents<IHoverable>();

        public IStartDraggable[] StartDraggables => GameObject.GetComponents<IStartDraggable>();

        public IDraggable[] Draggables => GameObject.GetComponents<IDraggable>();

        public IStopDraggable[] StopDraggables => GameObject.GetComponents<IStopDraggable>();

        public IUsable[] Usables => GameObject.GetComponents<IUsable>();



        public static bool IsInteractable(GameObject gameObject)
            => gameObject.GetComponent<IInteractable>() != null;



        public void Hover() => ForEach(Hoverables, x => x.Hover());

        public void Unhover() => ForEach(Hoverables, x => x.Unhover());

        public void StartDragging() => ForEach(StartDraggables, x => x.StartDragging());

        public void Drag() => ForEach(Draggables, x => x.Drag());

        public void StopDragging() => ForEach(StopDraggables, x => x.StopDragging());

        public void Use() => ForEach(Usables, x => x.Use());



        private void ForEach<T>(T[] array, Action<T> value)
        {
            for (int i = 0; i < array.Length; i++)
                value.Invoke(array[i]);
        }
    }
}
