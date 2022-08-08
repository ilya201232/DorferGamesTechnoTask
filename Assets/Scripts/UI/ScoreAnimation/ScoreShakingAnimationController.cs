using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.ScoreAnimation
{
    public class ScoreShakingAnimationController : MonoBehaviour
    {
        [SerializeField] private ScoreAnimationsSharedData animationsSharedData;
        
        [SerializeField] [Min(0.1f)] private float shakeIntensity;
        [SerializeField] [Min(0.1f)] private float shakeDecay;

        private float _currentShakeIntensity;
        private int _screenSizeParam;
        
        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = transform.position;
            _screenSizeParam = Screen.width / 80;
        }

        private void Update()
        {
            if (animationsSharedData.shakingAnimationOn)
            {
                _currentShakeIntensity = shakeIntensity * _screenSizeParam;
            }
            
            if (_currentShakeIntensity > 0)
            {
                var direction = Random.insideUnitCircle * _currentShakeIntensity;
                transform.position = _originalPosition + new Vector3(direction.x, direction.y, 0);

                _currentShakeIntensity -= shakeDecay * _screenSizeParam;
            }
            else
            {
                transform.position = _originalPosition;
            }
        }
    }
}