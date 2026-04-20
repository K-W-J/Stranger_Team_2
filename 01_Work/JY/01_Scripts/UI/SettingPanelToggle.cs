using _01_Work.HS.Core;
using UnityEngine;

public class SettingPanelToggle : MonoSingleton<SettingPanelToggle>
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject detailsPanel;

    [SerializeField] private GameObject clickBlockPanel;

    private bool onSettingPanel = false;
    private bool ondetailsUI = false;

    private void Awake()
    {
        settingPanel.SetActive(false);
        detailsPanel.SetActive(false);
        clickBlockPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onSettingPanel)
                if(!ondetailsUI) OpenSettingPanel();
                else OpendetailsSetting();

            else
                if (!ondetailsUI) CloseSettingPanel();
                else ClosedetailsSetting();
        }
    }


    public void ExitGame() => Application.Quit();

    public void OpenSettingPanel()
    {
        onSettingPanel = true;
        settingPanel.SetActive(true);
        clickBlockPanel.SetActive(true);
    }

    public void OpendetailsSetting()
    {
        ondetailsUI = true;
        detailsPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        onSettingPanel = false;
        settingPanel.SetActive(false);
        clickBlockPanel.SetActive(false);
    }

    public void ClosedetailsSetting()
    {
        ondetailsUI = false;
        detailsPanel.SetActive(false);
    }
}
