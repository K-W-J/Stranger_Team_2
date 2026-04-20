using _01_Work.HS.Core;
using TMPro;
using UnityEngine;

public class BuildWarningText : MonoSingleton<BuildWarningText>
{
    [SerializeField] private GameObject warningText;

    private void Awake()
    {
        ShowWarningText("", false);
    }

    public void ShowWarningText(string warningT, bool isBool)
    {
        warningText.SetActive(isBool);
        if (isBool)
            warningText.GetComponentInChildren<TMP_Text>().text = warningT;
    }
}
