using _01_Work.HS.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SmallAlarmChat : MonoSingleton<SmallAlarmChat>
{
    public GameObject chatMessagePrefab;
    public Transform content;
    public ScrollRect scrollRect;

    private void Update()
    {
        // if (Keyboard.current.digit1Key.wasPressedThisFrame)
        // {
        //     ResourceManager.Instance.AddResorce(ResourceType.GOLD, 10000);
        //     ResourceManager.Instance.AddResorce(ResourceType.WOOD, 10000);
        //     ResourceManager.Instance.AddResorce(ResourceType.STONE, 10000);
        //     ResourceManager.Instance.AddResorce(ResourceType.CRYSTAL, 10000);
        //     ResourceManager.Instance.AddResorce(ResourceType.FOOD, 10000);
        // }
    }

    public void AddChatMessage(string message)
    {
        GameObject newMessage = Instantiate(chatMessagePrefab, content);
        newMessage.GetComponentInChildren<TMP_Text>().text = message;

        newMessage.transform.SetParent(content, false);
        newMessage.transform.SetAsFirstSibling();

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

}
