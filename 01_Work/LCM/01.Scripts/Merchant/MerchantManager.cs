using System.Collections;
using _01_Work.LCM._01.Scripts.Day;
using UnityEngine;
using UnityEngine.Events;

namespace _01_Work.LCM._01.Scripts.Merchant
{
    public class MerchantManager : MonoBehaviour
    {
        [SerializeField] private int merchantCreatePercent;
        [SerializeField] private int addMerchantPercent;
        private int _defaultMerchantPercent;
        [SerializeField] private string chat;

        public UnityEvent OnMerchantCreated;

        private void Start()
        {
            DayManager.Instance.OnChangeMorning += MerchantCreatePercentUp;
            _defaultMerchantPercent = merchantCreatePercent;
        }

        private IEnumerator CheckCreateMerchant()
        {
            int rand = Random.Range(0, 100);
            if (rand <= merchantCreatePercent)
            {
                BigAlarmChat.Instance.AddChatMessage(chat);
                DayManager.Instance.IsTimeStopping = true;
                yield return new WaitForSeconds(3f);
                merchantCreatePercent = _defaultMerchantPercent;
                OnMerchantCreated?.Invoke();
            }
            else
                merchantCreatePercent += addMerchantPercent;
        }

        private void MerchantCreatePercentUp()
        {
            if (DayManager.Instance.CurrentDay >= 10)
            {
                StartCoroutine(CheckCreateMerchant());
            }
        }
    }
}