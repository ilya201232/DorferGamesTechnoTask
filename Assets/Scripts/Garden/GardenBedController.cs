using System;
using System.Collections;
using Crop;
using UnityEngine;

namespace Garden
{
    public class GardenBedController : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float delayBeforeCanCut;
        [SerializeField] [Min(0.1f)] private float delayBeforeGrowAgain;

        [SerializeField] private Color color;

        [SerializeField] [Min(0.1f)] private float initialBlockElevation;
        [SerializeField] [Min(0.1f)] private float blockFallingTime;

        private CropBlocksList _cropBlocksList;

        private bool _canBeCut;
        private ObjectPool _cubesPool;

        private Coroutine _waitUntilCanBeCutCoroutine;

        private PlaneCutter _planeCutter;

        private CutterCommonData _cutterCommonData;

        private void Start()
        {
            _canBeCut = true;
            _planeCutter = GetComponent<PlaneCutter>();
        }

        public void Initialise(CropBlocksList cropBlocksList, ObjectPool cubesPool)
        {
            _cropBlocksList = cropBlocksList;
            _cubesPool = cubesPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canBeCut) return;

            if (other.CompareTag("Player")) return;

            _planeCutter.Cut();

            _cutterCommonData ??= _planeCutter.CommonData;

            _cutterCommonData.AmountOfCutsLeft--;
            _canBeCut = false;

            if (_cutterCommonData.AmountOfCutsLeft == 0)
            {
                var cube = _cubesPool.GetFreeObject();
                cube.transform.position = transform.position +
                                          Vector3.up * (cube.transform.localScale.y / 2f + initialBlockElevation);

                var cropBlockController = cube.GetComponent<CropBlockController>();
                cropBlockController.SetBlockColor(color);

                cube.SetActive(true);

                StartCoroutine(Wait(blockFallingTime,
                    () => { _cropBlocksList.AddBlock(cropBlockController); },
                    () =>
                    {
                        cropBlockController.MoveBlock(cube.transform.position + Vector3.down * initialBlockElevation,
                            blockFallingTime);
                    }));


                StartCoroutine(Wait(delayBeforeGrowAgain, () =>
                {
                    _planeCutter.RestoreOriginal();

                    _cutterCommonData.AmountOfCutsLeft = _cutterCommonData.NeededAmountOfCuts;

                    if (_waitUntilCanBeCutCoroutine != null)
                    {
                        StopCoroutine(_waitUntilCanBeCutCoroutine);
                    }

                    _canBeCut = true;
                }));
            }
            else
            {
                _waitUntilCanBeCutCoroutine = StartCoroutine(Wait(delayBeforeCanCut, () => { _canBeCut = true; }));
            }
        }

        private static IEnumerator Wait(float time, Action actionAfter, Action actionBefore = null)
        {
            actionBefore?.Invoke();
            yield return new WaitForSeconds(time);
            actionAfter?.Invoke();
        }
    }
}