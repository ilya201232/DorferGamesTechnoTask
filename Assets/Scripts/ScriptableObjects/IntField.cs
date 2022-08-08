using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class IntField : ScriptableObject, ISerializationCallbackReceiver
    {
        
        public int value;

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            value = 0;
        }
    }
}