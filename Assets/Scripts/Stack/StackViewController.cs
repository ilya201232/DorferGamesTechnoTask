using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Stack
{
    public class StackViewController : MonoBehaviour
    {
        private struct CubeData
        {
            public Vector3Int Position { get; }
            public GameObject ThisGameObject { get; }

            public CubeData(Vector3Int position, GameObject thisGameObject)
            {
                Position = position;
                ThisGameObject = thisGameObject;
            }
        }

        [SerializeField] private StackSharedData stackSharedData;
        [SerializeField] private ObjectPool cubesObjectPool;
        [SerializeField] private GameObject blockPrefab;
        [SerializeField] private GameObject startPoint;

        [Tooltip("How many blocks can fit in x axis (red arrow)")] [SerializeField] [Min(1)]
        private int stackSizeX;

        [Tooltip("How many blocks can fit in z axis (blue arrow, but opposite direction)")] [SerializeField] [Min(1)]
        private int stackSizeZ;

        private Vector3Int _lastPosition;
        private float _cubeSize;

        private int _currentBlocksAmount;

        private Stack<CubeData> _cubesInStack;

        private void Start()
        {
            _lastPosition = new Vector3Int();
            _cubesInStack = new Stack<CubeData>();
            _currentBlocksAmount = stackSharedData.CurrentBlocksAmount;
            _cubeSize = blockPrefab.transform.localScale.x;

            _lastPosition = new Vector3Int(0, 0, 0);
        }

        private void Update()
        {
            if (_currentBlocksAmount == stackSharedData.CurrentBlocksAmount) return;

            if (_currentBlocksAmount < stackSharedData.CurrentBlocksAmount)
            {
                var newBlockColor = stackSharedData.BlockStack.Peek();
                var newBlock = cubesObjectPool.GetFreeObject();
                newBlock.GetComponentInChildren<MeshRenderer>().material.color = newBlockColor;
                
                newBlock.transform.position = startPoint.transform.position + transform.right * (_lastPosition.x * _cubeSize) -
                    transform.forward * (_lastPosition.z * _cubeSize) + transform.up * (_lastPosition.y * _cubeSize);

                newBlock.SetActive(true);
                
                _cubesInStack.Push(new CubeData(_lastPosition, newBlock));
                _currentBlocksAmount++;
                
                _lastPosition.x++;
                if (_lastPosition.x > stackSizeX - 1)
                {
                    _lastPosition.x = 0;
                    _lastPosition.z++;
                    if (_lastPosition.z > stackSizeZ - 1)
                    {
                        _lastPosition.z = 0;
                        _lastPosition.y++;
                    }
                }
            }
            else
            {
                var deletedCubeData = _cubesInStack.Pop();

                _lastPosition = deletedCubeData.Position;
                deletedCubeData.ThisGameObject.SetActive(false);
                _currentBlocksAmount--;
            }
        }

        private void AddBlock(Color color)
        {
        }
    }
}