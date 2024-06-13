using UnityEngine.InputSystem;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sources
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class SwitchProcessor : InputProcessor<float>
    {
#if UNITY_EDITOR
        static SwitchProcessor() => InputSystem.RegisterProcessor<SwitchProcessor>("Switch");
#endif

        private float _currentValue;
        
        public override float Process(float value, InputControl control)
        {
            if (value == 1)
                _currentValue = _currentValue == 0 ? 1 : 0;

            return _currentValue;
        }
    }
}
