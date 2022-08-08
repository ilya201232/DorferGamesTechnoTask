using ScriptableObjects;
using UnityEngine;

namespace Barn
{
    public class BarnController : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int cropSellingPrice;
        [SerializeField] private IntField score;

        public void SellBlock()
        {
            score.value += cropSellingPrice;
        }
    }
}