using System;
using _01_Work.HS.Core;
using DG.Tweening;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Day
{
    public class DayManager : MonoSingleton<DayManager>
    {
        [SerializeField] private Light directionalLight;
        private bool _isMorning = true;
        public event Action OnChangeMorning;
        public event Action OnChangeNight;
        [SerializeField] private float changeMorningAndNightTime;

        [SerializeField] private float timeBetweenMorningAndNight;
        private float _currentTime;

        public int CurrentDay { get; private set; } = 1;

        public bool IsTimeStopping { get; set; } = true;
        private int _beforeMoney;

        private void RotateSun()
        {
            Vector3 currentRotation = directionalLight.transform.rotation.eulerAngles;
            Vector3 targetRotation = new Vector3(currentRotation.x + 180f, 0, 0);

            _isMorning = !_isMorning;

            directionalLight.transform.DORotate(targetRotation, changeMorningAndNightTime);

            if (_isMorning)
            {
                CurrentDay++;
                
                OnChangeMorning?.Invoke();

                SmallAlarmChat.Instance.AddChatMessage(
                    $"아침이 밝았습니다. <b>{CurrentDay}</b>번째 날입니다.");
                
                int curGold = ResourceManager.Instance.GetCurResource(ResourceType.GOLD);
                if (_beforeMoney < curGold) {
                    SmallAlarmChat.Instance.AddChatMessage(
                        $"전날 골드보다 <color=yellow>{curGold-_beforeMoney}원의 골드</color>를 <color=green>획득</color>하였습니다.");
                }
                else if (_beforeMoney > curGold) {
                    SmallAlarmChat.Instance.AddChatMessage(
                        $"전날 골드보다 <color=yellow>{_beforeMoney-curGold}원의 골드</color>를 <color=red>소모</color>하였습니다.");
                }
                else {
                    SmallAlarmChat.Instance.AddChatMessage(
                        $"전날 골드와 현재 골드의 수가 동일합니다.");
                }

                _beforeMoney = curGold;
            }
            else OnChangeNight?.Invoke();
        }

        private void Update()
        {
            if (IsTimeStopping) return;

            _currentTime += Time.deltaTime;
            if (_currentTime >= timeBetweenMorningAndNight)
            {
                ChangeDayPos.Instance.ChangePos();
                RotateSun();
                _currentTime = 0;
            }
            FillImage();
        }

        private void FillImage()
        {
            if (_isMorning)
                ChangeDayPos.Instance.dayImage.fillAmount = _currentTime / timeBetweenMorningAndNight;
            else
            {
                ChangeDayPos.Instance.nightImage.fillAmount = _currentTime / timeBetweenMorningAndNight;
            }
        }

        public void StartTime()
        {
            IsTimeStopping = false;
            _beforeMoney = 100;
        }
    }
}