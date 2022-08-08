using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.JoyStick
{
    public class JoyStickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Min(1)] [SerializeField] private float movementRange = 50;
        [Range(0, 1)] [SerializeField] private float deadzone;
        [SerializeField] private InputData inputData;
        [SerializeField] private RectTransform backgroundTransform;
        [SerializeField] private RectTransform handleTransform;

        private Vector2 _position = Vector2.zero;

        public void OnPointerDown(PointerEventData eventData)
        {
            _position = eventData.position;
            backgroundTransform.position = eventData.position;
            backgroundTransform.gameObject.SetActive(true);
            handleTransform.anchoredPosition = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            backgroundTransform.gameObject.SetActive(false);
            handleTransform.anchoredPosition = Vector2.zero;
            inputData.joystickData = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log(eventData.position);
            var direction = eventData.position - _position;

            direction = (direction.magnitude > movementRange / 2f) ? direction.normalized * movementRange / 2f : direction;
            // direction = Vector2.ClampMagnitude(direction, movementRange/2f);

            direction = (direction.magnitude < movementRange * deadzone) ? Vector2.zero : direction;

            handleTransform.anchoredPosition = direction;
            inputData.joystickData = direction / movementRange * 2f;
        }
    }
}