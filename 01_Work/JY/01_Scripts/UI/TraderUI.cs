using _01_Work.LCM._01.Scripts.Day;
using UnityEngine;
using UnityEngine.Events;

public class TraderUI : MonoBehaviour
{
    [SerializeField] private GameObject traderUI;
    
    [SerializeField] private GameObject merchant;
    [SerializeField] private ParticleSystem dustParticle;

    public UnityEvent OnDeadEvent;

    public void ToggleTraderUI()
    {
        OnDeadEvent?.Invoke();
        DayManager.Instance.IsTimeStopping = false;
    }
}
