using UnityEngine;

public class PriorityPanelToggle : MonoBehaviour
{
    [SerializeField] private GameObject priorityPanel;

    private void Start()
    {
        priorityPanel.SetActive(false);
    }

    public void PanelEnable()
    {
        priorityPanel.SetActive(true);
    }

    public void PanelDisable()
    {
        priorityPanel.SetActive(false);
    }
}
