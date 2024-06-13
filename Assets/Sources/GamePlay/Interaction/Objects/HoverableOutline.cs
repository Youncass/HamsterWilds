using QuickOutline;
using UnityEngine;

namespace Sources.GamePlay.Interaction
{
    public class HoverableOutline : MonoBehaviour, IHoverable
    {
        [SerializeField] private Outline _outline;

        public void Hover() => _outline.enabled = true;

        public void Unhover() => _outline.enabled = false;

        private void OnValidate()
        {
            if (_outline)
                _outline.enabled = false;
        }
    }
}
