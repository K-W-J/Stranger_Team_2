using _01_Work.HS.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BigAlarmChat : MonoSingleton<BigAlarmChat>
{
    public GameObject chatMessagePrefab; // 메시지 프리팹
    public Transform content; // Content 오브젝트
    public ScrollRect scrollRect;

    public void AddChatMessage(string message)
    {
        GameObject newMessage = Instantiate(chatMessagePrefab, content);
        newMessage.GetComponentInChildren<TMP_Text>().text = message;

        // 레이아웃 강제 갱신 + 스크롤 아래로 이동
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
