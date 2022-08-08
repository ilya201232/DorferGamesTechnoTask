using Crop;
using UnityEngine;

namespace Garden
{
    public class GardenController : MonoBehaviour
    {
        [SerializeField] [Min(1)] [Tooltip("How much will garden grow in red axis")]
        private int gardenWidth;

        [SerializeField] [Min(1)] [Tooltip("How much will garden grow in blue axis")]
        private int gardenHeight;

        [SerializeField] private GameObject gardenBedPrefab;
    
        [SerializeField]
        private CropBlocksList cropBlocksList;
        
        [SerializeField] private ObjectPool cubesPool;

        private void Start()
        {
            var transform1 = transform;
            for (var i = 0; i < gardenHeight; i++)
            {
                for (var j = 0; j < gardenWidth; j++)
                {
                    var obj = Instantiate(gardenBedPrefab, transform1.position + transform1.forward * i + transform1.right * j,
                        Quaternion.Euler(0, 0, 0));
                    obj.transform.parent = transform1;
                    obj.GetComponent<GardenBedController>().Initialise(cropBlocksList, cubesPool);
                }
            }
        }
    }
}