using System;
using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private IntField score;
        [SerializeField] private int maxScoreLength;

        private int _currentScore;

        private void Start()
        {
            _currentScore = score.value;
        }

        private void Update()
        {
            if (score.value == _currentScore) return;
            
            _currentScore = score.value;
            if (_currentScore.ToString().Length > maxScoreLength)
            {
                _currentScore = int.Parse(new string('9', maxScoreLength));
                score.value = _currentScore;
            }
            
        }

        
    }
}