using System;
using System.Collections;
using Barn;
using Crop;
using ScriptableObjects;
using UnityEngine;

namespace Stack
{
    public class StackController : MonoBehaviour
    {
        
        [Space] [Header("Player reference")] [SerializeField]
        private Transform playerTransform;

        [Space] [Header("Shared data")] [SerializeField] 
        private StackSharedData stackSharedData;

        [Space] [Header("Grab action params")] [SerializeField] [Min(0.1f)]
        private float cubeGrabDistance;
        [SerializeField] [Min(0.1f)] 
        private float cubeGrabDuration;
        [SerializeField] 
        private CropBlocksList cropBlocksList;

        [Space] [Header("Send action params")] [SerializeField] [Min(0.1f)]
        private float delayBeforeSendingBlock;
        [SerializeField] [Min(0.1f)] private float blockSendingDuration;
        [SerializeField] private ObjectPool cubesPool;
        [SerializeField] private BarnController barnController;

        private bool _canRemove;

        private int _expectedFinalBlockAmount;

        private void Start()
        {
            _expectedFinalBlockAmount = 0;

            _canRemove = true;
        }

        private void Update()
        {
            TryGettingBlock();
        }

        private void TryGettingBlock()
        {
            if (cropBlocksList.BlockCount() == 0) return;
            if (stackSharedData.CurrentBlocksAmount == stackSharedData.MaxBlockAmount) return;

            foreach (var cropBlockController in
                     cropBlocksList.GetBlocksCloseToTheObject(transform.position, cubeGrabDistance,
                         stackSharedData.MaxBlockAmount - _expectedFinalBlockAmount))
            {
                _expectedFinalBlockAmount++;
                StartCoroutine(AddBlockToTheStack(cropBlockController));
            }
        }

        private IEnumerator AddBlockToTheStack(CropBlockController cropBlockController)
        {
            var cubeTransform = cropBlockController.transform;

            cropBlockController.MoveBlock(playerTransform.position + Vector3.up * playerTransform.localScale.x -
                                          (playerTransform.position - cubeTransform.position).normalized *
                                          (Mathf.Sqrt(3) * cubeTransform.localScale.x),
                cubeGrabDuration);

            yield return new WaitForSeconds(cubeGrabDuration);

            cropBlockController.gameObject.SetActive(false);
            AddBlock(cropBlockController.BlockColor);
        }

        private void AddBlock(Color color)
        {
            if (stackSharedData.CurrentBlocksAmount >= stackSharedData.MaxBlockAmount)
                throw new Exception("Can't add new block to the stack.");
            stackSharedData.CurrentBlocksAmount++;
            stackSharedData.BlockStack.Push(color);
        }

        public void TrySendingBlock(Vector3 inputPoint)
        {
            if (!_canRemove) return;

            if (stackSharedData.CurrentBlocksAmount <= 0) return;

            var cropBlockController = cubesPool.GetFreeObject().GetComponent<CropBlockController>();

            cropBlockController.SetBlockColor(RemoveBlock());

            var cropTransform = cropBlockController.transform;
            cropTransform.position = playerTransform.position
                                     + Vector3.up * playerTransform.localScale.x
                                     + Vector3.up * cropTransform.localScale.y / 2f;

            cropTransform.position += (inputPoint - cropTransform.position).normalized *
                                      Mathf.Sqrt(3) * cropBlockController.transform.localScale.x;
            cropBlockController.gameObject.SetActive(true);

            _canRemove = false;
            StartCoroutine(MoveBlockToTheBarn(cropBlockController, inputPoint));
        }

        private IEnumerator MoveBlockToTheBarn(CropBlockController cropBlockController, Vector3 inputPoint)
        {
            var cubeTransform = cropBlockController.transform;

            cropBlockController.MoveBlock(
                inputPoint - (inputPoint - cubeTransform.position).normalized *
                (Mathf.Sqrt(3) * cubeTransform.localScale.x), blockSendingDuration);

            yield return new WaitForSeconds(blockSendingDuration);

            cropBlockController.gameObject.SetActive(false);

            barnController.SellBlock();

            yield return new WaitForSeconds(delayBeforeSendingBlock);
            _canRemove = true;
        }

        private Color RemoveBlock()
        {
            if (stackSharedData.CurrentBlocksAmount <= 0) throw new Exception("Can't remove any block from the stack.");
            stackSharedData.CurrentBlocksAmount--;
            _expectedFinalBlockAmount--;

            return stackSharedData.BlockStack.Pop();
        }
    }
}