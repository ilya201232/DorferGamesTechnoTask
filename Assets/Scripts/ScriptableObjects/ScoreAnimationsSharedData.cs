using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class ScoreAnimationsSharedData : ScriptableObject, ISerializationCallbackReceiver
    {
        public bool shakingAnimationOn;
        public bool addingMoneyAnimationTrigger;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            shakingAnimationOn = false;
            addingMoneyAnimationTrigger = false;
        }
    }
}