using System.Collections.Generic;
using UnityEngine;

namespace Crop
{
    public class CropBlocksList : MonoBehaviour
    {
        private List<CropBlockController> _cropBlockControllers;

        private void Start()
        {
            _cropBlockControllers = new List<CropBlockController>();
        }

        public void AddBlock(CropBlockController cropBlockController)
        {
            _cropBlockControllers.Add(cropBlockController);
        }
    
        public int BlockCount()
        {
            return _cropBlockControllers.Count;
        }

        public List<CropBlockController> GetBlocksCloseToTheObject(Vector3 objectPosition, float maxDistance, int maxBlocksToGet)
        {

            if (maxBlocksToGet <= 0) return new List<CropBlockController>();

            var list = new List<CropBlockController>();

            foreach (var cropBlockController in _cropBlockControllers)
            {
                if (Vector3.Distance(cropBlockController.transform.position, objectPosition) <= maxDistance)
                {
                    list.Add(cropBlockController);
                }

                if (list.Count == maxBlocksToGet)
                {
                    break;
                }
            }

            foreach (var cropBlockController in list)
            {
                _cropBlockControllers.Remove(cropBlockController);
            }

            return list;
        }
    }
}