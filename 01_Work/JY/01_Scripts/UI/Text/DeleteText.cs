using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DeleteText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private CanvasGroup group;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        StartCoroutine(TextCoolTime());
    }

    private IEnumerator TextCoolTime()
    {
        yield return new WaitForSeconds(60);
        group.DOFade(0, 1).OnComplete(() => DestroyObject());
        
    }

    private void DestroyObject()
    {
        Destroy(text);
        Destroy(gameObject);
    }
}
