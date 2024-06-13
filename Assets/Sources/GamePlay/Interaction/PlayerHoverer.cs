using Sources.StaticData;
using System;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

namespace Sources.GamePlay.Interaction
{
    public class PlayerHoverer : IDisposable
    {
        private InteractionObject _object;

        private readonly Transform _lookPoint;
        private readonly PlayerInteractor _interactor;
        private readonly PlayerHovererData _data;

        private readonly CompositeDisposable _disposables = new();

        public PlayerHoverer(Transform lookPoint, PlayerInteractor interactor, PlayerHovererData data)
        {
            _lookPoint = lookPoint;
            _interactor = interactor;
            _data = data;

            _lookPoint.gameObject
                .UpdateAsObservable()
                .Subscribe(x => Raycast())
                .AddTo(_disposables);
        }

        private void Raycast()
        {
            var look = new Ray(
                origin: _lookPoint.position,
                direction: _lookPoint.forward);

            if (Physics.Raycast(look, out RaycastHit hit, _data.InteractionDistance, _data.InteractionMask))
            {
                GameObject newObject = hit.transform.gameObject;

                if (_object == null || _object.Equals(newObject) == false)
                    TryHoverToObject(newObject);
            }
            else UnhoverFromObject();
        }

        private void TryHoverToObject(GameObject newObject)
        {
            if (InteractionObject.IsInteractable(newObject))
                HoverToNewObject(newObject);
            else
                UnhoverFromObject();
        }

        private void HoverToNewObject(GameObject newObject)
        {
            UnhoverFromObject();

            _object = new InteractionObject(newObject);
            _object.Hover();

            _interactor.SuggestInteraction(_object);
        }

        private void UnhoverFromObject()
        {
            _object?.Unhover();
            _object = null;

            _interactor.RemoveSuggestion();
        }

        public void Dispose() => _disposables.Dispose();
    }
}
