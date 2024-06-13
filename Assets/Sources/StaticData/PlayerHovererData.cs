using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu]
    public class PlayerHovererData : ScriptableObject
    {
        [Range(0f, 10f)] public float InteractionDistance = 2f;
        public LayerMask InteractionMask;
    }
}
