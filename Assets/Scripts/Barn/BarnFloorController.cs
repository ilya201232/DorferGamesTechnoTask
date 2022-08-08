using Player;
using Stack;
using UnityEngine;

namespace Barn
{
    public class BarnFloorController : MonoBehaviour
    {
        [SerializeField]
        private Transform barnInputPointTransform;

        private StackController _stackController;

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
        
            _stackController ??= other.GetComponentInChildren<StackController>();

            _stackController.TrySendingBlock(barnInputPointTransform.position);
        }
    }
}