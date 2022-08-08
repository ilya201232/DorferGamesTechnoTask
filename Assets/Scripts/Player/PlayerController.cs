using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private PlayerAnimationSharedData playerAnimationSharedData;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 direction)
        {
            if (!playerAnimationSharedData.isHarvestAnimationInProgress)
            {
                if (!direction.Equals(Vector3.zero))
                {
                    TurnPlayer(direction);

                    var tmpDirection = new Vector3(direction.x, 0, direction.y);
                    _characterController.Move(tmpDirection * (speed * Time.deltaTime));
                    playerAnimationSharedData.isRunning = true;
                    playerAnimationSharedData.runningSpeed = tmpDirection.magnitude * speed / 2f;
                }
                else
                {
                    playerAnimationSharedData.isRunning = false;
                }
            }
        }

        public void Harvest()
        {
            if (!playerAnimationSharedData.isHarvestAnimationInProgress)
            {
                playerAnimationSharedData.harvestingTrigger = true;
            }
        }
    
        private void TurnPlayer(Vector2 turningVector)
        {
            var angle = Mathf.Atan2(turningVector.y, turningVector.x) * Mathf.Rad2Deg;
            angle = -angle - transform.rotation.eulerAngles.y + 90f;

            transform.Rotate(Vector3.up, angle);
        }
    }
}
