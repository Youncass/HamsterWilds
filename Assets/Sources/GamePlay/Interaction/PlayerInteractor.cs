using Sources.Services.Input;
using UnityEngine;
using Zenject;
using UniRx;
using System;

namespace Sources.GamePlay.Interaction
{
    public class PlayerInteractor : IDisposable
    {
        private InteractionObject _object;

        private readonly IPlayerInteractorView _view;
        private readonly CompositeDisposable _disposables = new();

        public PlayerInteractor(GameObject view, IPlayerInput input)
        {
            _view = view.GetComponentChecked<IPlayerInteractorView>();

            input.Using
                .Where(x => _object != null && x)
                .Subscribe(x => _object.Use())
                .AddTo(_disposables);
        }

        public void SuggestInteraction(InteractionObject interactionObject)
        {
            _object = interactionObject;
            _view.DisplayInteractionHints(_object);
        }

        public void RemoveSuggestion()
        {
            _object = null;
            _view.RemoveInteractionHints();
        }

        public void Dispose() => _disposables.Dispose();
    }
}
