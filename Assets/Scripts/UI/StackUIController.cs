using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StackUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textElement;
        [SerializeField] private StackSharedData stackSharedData;

        private int _currentCurAmount;
        private int _currentMaxAmount;

        private void Start()
        {
            _currentCurAmount = 0;
            _currentMaxAmount = stackSharedData.MaxBlockAmount;
            textElement.text = $"{stackSharedData.CurrentBlocksAmount}/{stackSharedData.MaxBlockAmount}";
        }

         private void Update()
         {
             if (stackSharedData.CurrentBlocksAmount == _currentCurAmount && stackSharedData.MaxBlockAmount == _currentMaxAmount) return;

             textElement.text = $"{stackSharedData.CurrentBlocksAmount}/{stackSharedData.MaxBlockAmount}";

             _currentCurAmount = stackSharedData.CurrentBlocksAmount;
             _currentMaxAmount = stackSharedData.MaxBlockAmount;
         }
    }
}