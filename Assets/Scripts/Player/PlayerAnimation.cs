using ScriptableObjects;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject scytheObject;
        [SerializeField] private Rig rig;

        [SerializeField] private PlayerAnimationSharedData sharedData;
    
        private float _harvestAnimationPositionCorrectionSpeed = -1;
        
        private CharacterController _characterController;
    
        private Animator _animator;
        private int _isRunningParameterHash;
        private int _harvestTriggerHash;
        private int _runningAnimationSpeedHash;
        
        private bool _canChangeIsInProgress;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            
            _isRunningParameterHash = Animator.StringToHash("IsRunning");
            _runningAnimationSpeedHash = Animator.StringToHash("RunningAnimationSpeed");
            _harvestTriggerHash = Animator.StringToHash("Harvest");
            
            _canChangeIsInProgress = false;
        }

        private void Update()
        {
            if (sharedData.isRunning)
            {
                _animator.SetBool(_isRunningParameterHash, true);
                _animator.SetFloat(_runningAnimationSpeedHash, sharedData.runningSpeed);
            }
            else
            {
                _animator.SetBool(_isRunningParameterHash, false);
            }

            if (sharedData.harvestingTrigger)
            {
                sharedData.isHarvestAnimationInProgress = true;
                sharedData.harvestingTrigger = false;
                _canChangeIsInProgress = false;
                _animator.SetTrigger(_harvestTriggerHash);
            }
            
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Harvesting"))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.88f)
                {
                    if (rig.weight > 0.5f)
                    {
                        rig.weight = 0;
                    }
            
                    if (scytheObject.activeInHierarchy)
                    {
                        scytheObject.SetActive(false);
                    }
                
                    _canChangeIsInProgress = true;
                }
                else
                {
                    if (rig.weight < 0.5f)
                    {
                        rig.weight = 1;
                    }

                    if (!scytheObject.activeInHierarchy)
                    {
                        scytheObject.SetActive(true);
                    }
                }
            
                if (_harvestAnimationPositionCorrectionSpeed < 0)
                    _harvestAnimationPositionCorrectionSpeed = 0.7f / _animator.GetCurrentAnimatorStateInfo(0).length;

                _characterController.Move(transform.forward *
                                          (_harvestAnimationPositionCorrectionSpeed * Time.deltaTime));
            }
            else
            {
                if (_canChangeIsInProgress && sharedData.isHarvestAnimationInProgress)
                {
                    sharedData.isHarvestAnimationInProgress = false;
                }
            }
        }
    }
}
