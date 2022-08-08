using EzySlice;
using UnityEngine;

namespace Garden
{
    public class PlaneCutter : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float baseHeight;
        [SerializeField] [Range(0, 1)] private float topPartCut;

        [SerializeField] [Min(1)] private int neededAmountOfCuts;

        [SerializeField] private GameObject objectToCut;
    
        [SerializeField] private Material fadeMaterial;

        public CutterCommonData CommonData { get; private set; }

        private GameObject _currentObjectToCut;

        private GameObject _originalObject;
        private float _originalObjectHeight;
        private bool _isOriginal;

        private void Start()
        {
            CommonData = new CutterCommonData(neededAmountOfCuts);
            _originalObjectHeight = GetComponent<BoxCollider>().size.y - topPartCut;

            _currentObjectToCut = objectToCut;
            _isOriginal = true;
        }

        public void Cut()
        {
            var hull = SliceObject(_currentObjectToCut);

            if (hull != null)
            {
                var lowerHull = hull.CreateLowerHull(_currentObjectToCut, fadeMaterial);

                lowerHull.transform.parent = transform;
                lowerHull.transform.localPosition = Vector3.zero;

                if (_isOriginal)
                {
                    _originalObject = _currentObjectToCut;
                    _originalObject.SetActive(false);
                    _isOriginal = false;
                }
                else
                {
                    Destroy(_currentObjectToCut);
                }

                _currentObjectToCut = lowerHull;
            }
        }

        public void RestoreOriginal()
        {
            if (!_isOriginal)
            {
                Destroy(_currentObjectToCut);
                _originalObject.SetActive(true);

                _currentObjectToCut = _originalObject;
                _isOriginal = true;
            }
        }

        private SlicedHull SliceObject(GameObject obj)
        {
            return obj.Slice(
                transform.position + transform.up * (baseHeight + (_originalObjectHeight - baseHeight) *
                    (CommonData.AmountOfCutsLeft - 1) / neededAmountOfCuts), transform.up);
        }
    }
}