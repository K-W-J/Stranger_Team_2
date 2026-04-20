using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.JY._01_Scripts.SO
{
    [CreateAssetMenu(fileName = "SoundList", menuName = "SO/Audio/list", order = 0)]
    public class SoundListSO : ScriptableObject
    {
        public List<SoundSO> soundDataList = new List<SoundSO>();
    }
}