using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Merchant
{
    public class MerchantFeedback : Feedback
    {
        [SerializeField] private GameObject merchant;

        public List<Transform> merchantPositions;
        public override void PlayFeedback()
        {
            int rand  = Random.Range(0, merchantPositions.Count);
            Instantiate(merchant, merchantPositions[rand].position, Quaternion.identity);
        }
    }
}
