using DG.Tweening;
using UnityEngine;

namespace Crop
{
    public class CropBlockController : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        public Color BlockColor { get; private set; }

        private void Awake()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void SetBlockColor(Color color)
        {
            BlockColor = color;
            _meshRenderer.material.color = color;
        }

        public void MoveBlock(Vector3 target, float movingDuration)
        {
            transform.DOMove(target, movingDuration);
        }
    }
}