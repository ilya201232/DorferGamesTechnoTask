using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerAnimationSharedData : ScriptableObject, ISerializationCallbackReceiver
    {
        public bool isHarvestAnimationInProgress;
        public bool isRunning;
        public float runningSpeed;
        public bool harvestingTrigger;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            isHarvestAnimationInProgress = false;
            isRunning = false;
            runningSpeed = 1;
            harvestingTrigger = false;
        }
    }
}