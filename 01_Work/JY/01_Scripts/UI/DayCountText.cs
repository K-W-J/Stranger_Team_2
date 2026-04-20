using System;
using _01_Work.LCM._01.Scripts.Day;
using TMPro;
using UnityEngine;

namespace _01_Work.JY._01_Scripts.UI
{
    public class DayCountText : MonoBehaviour
    {
        [SerializeField] private TMP_Text dayCountText;

        private DayManager _dayManager;
        
        private void Start()
        {
            _dayManager = DayManager.Instance;
            DayManager.Instance.OnChangeMorning += ChangeMorning;
            ChangeMorning();
        }

        private void ChangeMorning() => dayCountText.text = $"{_dayManager.CurrentDay}일";
    }
}