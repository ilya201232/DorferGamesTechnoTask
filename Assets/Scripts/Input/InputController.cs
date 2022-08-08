using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputData inputData;

        [SerializeField] private UnityEvent<Vector2> onJoystickMove;

        private void Update()
        {
            onJoystickMove.Invoke(inputData.joystickData);
        }
    }
}