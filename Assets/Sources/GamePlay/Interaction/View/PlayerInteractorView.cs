using UnityEngine;
using System.Linq;

namespace Sources.GamePlay.Interaction
{
    public class PlayerInteractorView : MonoBehaviour, IPlayerInteractorView
    {
        [SerializeField] private CanvasGroup _hintsMenu;
        [SerializeField] private GameObject _dragHint;
        [SerializeField] private GameObject _useHint;

        public void DisplayInteractionHints(InteractionObject interactionObject)
        {
            _hintsMenu.alpha = 1f;

            if (interactionObject.Draggables.Any())
                _dragHint.SetActive(true);

            if (interactionObject.Usables.Any())
                _useHint.SetActive(true);
        }

        public void RemoveInteractionHints()
        {
            _hintsMenu.alpha = 0f;
            _useHint.SetActive(false);
            _dragHint.SetActive(false);
        }
    }
}
