using System;
using System.Collections;
using System.Collections.Generic;
using _01_Work.HS.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Work.LCM._01.Scripts.Weather
{
    public class WeatherManager : MonoSingleton<WeatherManager>
    {
        public List<WeatherSO> weathers;

        [SerializeField] private float minSeasonChangeTime;
        [SerializeField] private float maxSeasonChangeTime;

        [SerializeField] private WeatherPosition weatherPosition;

        private Season _currentSeason = Season.Spring;
        private WeatherType _currentWeather;

        public event Action<WeatherType> OnChangeWeather;

        private void Start()
        {
            StartCoroutine(SeasonChangeCoroutine());
            StartCoroutine(WeatherChangeCoroutine());
        }


        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator WeatherChangeCoroutine()
        {
            List<WeatherSO> possibleWeathers = weathers.FindAll(w => w.season.HasFlag(_currentSeason));

            float totalProbability = 0f;
            foreach (var weather in possibleWeathers)
            {
                totalProbability += weather.probability;
            }

            float randomValue = Random.Range(0, totalProbability);
            float cumulativeProbability = 0f;

            WeatherSO selectedWeather = null;
            foreach (var weather in possibleWeathers)
            {
                cumulativeProbability += weather.probability;
                if (randomValue <= cumulativeProbability)
                {
                    selectedWeather = weather;
                    break;
                }
            }

            if (selectedWeather != null)
            {
                weatherPosition.ParticleDictionary[selectedWeather.name].Play();

                float rand = Random.Range(selectedWeather.minWeatherChangeTime, selectedWeather.maxWeatherChangeTime);
                _currentWeather = selectedWeather.weatherType;
                WeatherSound(selectedWeather);
                if (selectedWeather.weatherType != WeatherType.Sunny)
                    SmallAlarmChat.Instance.AddChatMessage(
                        $"현재 <color=lightblue>{selectedWeather.weatherName}</color>(이)가 옵니다.");
                Debug.Log(_currentWeather);
                OnChangeWeather?.Invoke(_currentWeather);
                yield return new WaitForSeconds(rand);
                weatherPosition.ParticleDictionary[selectedWeather.name]
                    .Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            yield return new WaitForSeconds(10f);

            StartCoroutine(WeatherChangeCoroutine());
        }

        private void WeatherSound(WeatherSO selectedWeather)
        {
            if (selectedWeather.weatherType == WeatherType.RedRain || selectedWeather.weatherType == WeatherType.HeavyRain ||
                selectedWeather.weatherType == WeatherType.MediumRain || selectedWeather.weatherType == WeatherType.LightRain)
            {
                AudioManager.Instance.PlayBGM("RAIN");
            }
            else if (selectedWeather.weatherType == WeatherType.HeavySnow || selectedWeather.weatherType == WeatherType.LightSnow ||
                selectedWeather.weatherType == WeatherType.MediumSnow)
            {
                AudioManager.Instance.PlayBGM("SNOW");
            }
            else
            {
                AudioManager.Instance.PlayBGM("DEFAULT");
            }
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator SeasonChangeCoroutine()
        {
            float rand = Random.Range(minSeasonChangeTime, maxSeasonChangeTime);
            yield return new WaitForSeconds(rand);

            _currentSeason = _currentSeason == Season.Winter ? Season.Spring : (Season)((int)_currentSeason << 1);

            StartCoroutine(SeasonChangeCoroutine());
        }
    }

    public enum WeatherType
    {
        Sunny,
        LightRain,
        MediumRain,
        HeavyRain,
        LightSnow,
        MediumSnow,
        HeavySnow,
        RedRain,
        Dust
    }

    [Flags]
    public enum Season
    {
        Spring = 1 << 1,
        Summer = 1 << 2,
        Fall = 1 << 3,
        Winter = 1 << 4
    }
}