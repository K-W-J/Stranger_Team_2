using UnityEditor;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Weather
{
    [CreateAssetMenu(fileName = "WeatherSO", menuName = "SO/WeatherSO")]
    public class WeatherSO : ScriptableObject
    {
        [Header("Weather")] 
        public WeatherType weatherType;
        public string weatherName;
        public Season season;

        [Header("Time")] 
        public float minWeatherChangeTime;
        public float maxWeatherChangeTime;


        [Header("Event")] 
        [Range(0,100)] public int probability;

        private void OnValidate()
        {
            name = weatherType.ToString();

#if UNITY_EDITOR
            EditorApplication.delayCall += () =>
            {
                string assetPath = AssetDatabase.GetAssetPath(this);
                if (!string.IsNullOrEmpty(assetPath))
                {
                    AssetDatabase.RenameAsset(assetPath, name);
                    AssetDatabase.SaveAssets();
                }
            };
#endif
        }
    }
}