using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class InputData : ScriptableObject, ISerializationCallbackReceiver
    {
        public Vector2 joystickData;

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            joystickData = Vector2.zero;
        }
    }
}