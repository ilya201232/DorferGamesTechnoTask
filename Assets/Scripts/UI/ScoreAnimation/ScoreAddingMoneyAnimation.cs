using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.ScoreAnimation
{
    public class ScoreAddingMoneyAnimation : MonoBehaviour
    {
        [SerializeField] private ScoreAnimationsSharedData animationsSharedData;
        [SerializeField] private TextMeshProUGUI textElement;
        [SerializeField] private IntField score;
        
        [SerializeField] [Min(0.1f)] private float timeToChangeValue;
        
        private int _currentScore;

        private Coroutine _changingCoroutine;

        private void Start()
        {
            textElement.text = _currentScore.ToString();
        }

        private void Update()
        {
            if (animationsSharedData.addingMoneyAnimationTrigger)
            {
                if (_changingCoroutine != null) StopCoroutine(_changingCoroutine);

                _changingCoroutine = StartCoroutine(ChangeValueSmoothly(_currentScore, score.value, timeToChangeValue));
                animationsSharedData.addingMoneyAnimationTrigger = false;
            }
        }
        
        private IEnumerator ChangeValueSmoothly(int start, int finish, float duration)
        {
            var passed = 0f;
            animationsSharedData.shakingAnimationOn = true;
            
            while (passed < duration)
            {
                _currentScore = (int) Mathf.Lerp(start, finish, passed / duration);
                textElement.text = _currentScore.ToString();
                passed += Time.deltaTime;
                yield return null;
            }

            _currentScore = finish;
            animationsSharedData.shakingAnimationOn = false;
            textElement.text = _currentScore.ToString();
        }
    }
}