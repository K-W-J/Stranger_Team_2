using UnityEngine;
using UnityEngine.Events;

namespace _01_Work.LCM._01.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "EventSO", menuName = "SO/EventSO")]
    public class EventSO : ScriptableObject
    {
        public UnityEvent OnGameEvtEvent;
        [Range(0, 100)] public int probability;

        public void InvokeEvent()
        {
            int random = Random.Range(0, 100);
            if (random < probability)
            {
                OnGameEvtEvent.Invoke();
            }
        }
    }
}
