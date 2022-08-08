using System.Collections;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace UI.ScoreAnimation
{
    public class ScoreMoneyFlyingAnimationController : MonoBehaviour
    {
        [SerializeField] private IntField score;
        [SerializeField] private ScoreAnimationsSharedData animationsSharedData;
        [SerializeField] private ObjectPool coinsPool;
        [SerializeField] private RectTransform startPoint;
        [SerializeField] private RectTransform finishPoint;

        [SerializeField] [Min(0)] private float coinMoveDelay;
        [SerializeField] [Min(0.1f)] private float coinMoveDuration;

        private int _currentScore;

        private void Start()
        {
            _currentScore = score.value;
        }

        private void Update()
        {
            if (score.value == _currentScore) return;

            StartCoroutine(MoveCoin());
            _currentScore = score.value;
        }

        private IEnumerator MoveCoin()
        {
            yield return new WaitForSeconds(coinMoveDelay);
            var coin = coinsPool.GetFreeObject();

            coin.transform.position = startPoint.position;
            
            coin.SetActive(true);

            coin.transform.DOMove(finishPoint.position, coinMoveDuration);
            yield return new WaitForSeconds(coinMoveDuration);
            
            coin.SetActive(false);
            
            animationsSharedData.addingMoneyAnimationTrigger = true;
        }
    }
}