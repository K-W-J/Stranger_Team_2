using UnityEngine;

namespace _01_Work.JY._01_Scripts.SO
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Audio/Data", order = 0)]
    public class SoundSO : ScriptableObject
    {
        public AudioManager.SoundType soundType;
        public AudioClip clip;
        public string soundName;
    }
}