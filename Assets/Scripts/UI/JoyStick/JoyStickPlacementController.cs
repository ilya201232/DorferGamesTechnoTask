/*
using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace UI.JoyStick
{
    public class JoyStickPlacementController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        
        [SerializeField] private RectTransform joyStickParentTransform;
        [SerializeField] private OnScreenStick joyStickController;
        
        private void OnEnable()
        {
            inputController.OnStartTouch += TryPlacingJoyStick;
            inputController.OnEndTouch += RemoveJoyStick;
        }

        private void OnDisable()
        {
            inputController.OnStartTouch -= TryPlacingJoyStick;
            inputController.OnEndTouch -= RemoveJoyStick;
        }

        private void RemoveJoyStick(Vector2 position, float time)
        {
            joyStickParentTransform.gameObject.SetActive(false);
        }

        private void TryPlacingJoyStick(Vector2 position, float time)
        {
            if (position.x < joyStickController.movementRange / 2)
            {
                position.x = joyStickController.movementRange / 2;
            }
            else if (position.x > Screen.width - joyStickController.movementRange / 2)
            {
                position.x = Screen.width - joyStickController.movementRange / 2;
            }
            
            if (position.y < joyStickController.movementRange / 2)
            {
                position.y = joyStickController.movementRange / 2;
            }
            else if (position.y > Screen.width - joyStickController.movementRange / 2)
            {
                position.y = Screen.height - joyStickController.movementRange / 2;
            }
            
            joyStickParentTransform.position = position;
            joyStickParentTransform.gameObject.SetActive(true);
        }
    }
}
*/
