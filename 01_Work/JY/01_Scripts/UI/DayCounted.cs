using System;
using _01_Work.LCM._01.Scripts.Day;
using TMPro;
using UnityEngine;

namespace _01_Work.JY._01_Scripts.UI
{
    public class DayCounted : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            //text.text = $"나라를 운영한 기간 : {DayManager.Instance.CurrentDay}일";
        }
    }
}