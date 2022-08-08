using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class StackSharedData : ScriptableObject, ISerializationCallbackReceiver
    {
        [field: SerializeField]
        public int CurrentBlocksAmount { get; set; }
        
        [field: SerializeField]
        public int MaxBlockAmount { get; set; }

        public Stack<Color> BlockStack { get; private set; }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            CurrentBlocksAmount = 0;
            BlockStack = new Stack<Color>();
        }
    }
}