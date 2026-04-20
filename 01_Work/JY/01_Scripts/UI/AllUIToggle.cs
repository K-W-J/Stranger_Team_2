using _01_Work.HS.Core;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AllUIToggle : MonoSingleton<AllUIToggle>
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject firstText;
    private List<RectTransform> gList = new List<RectTransform>();
    private List<RectTransform> oldList = new List<RectTransform>();

    private bool _isFirst = false;

    private void Awake()
    {
        foreach (RectTransform child in canvas.transform)
        {
            oldList.Add(child);
            gList.Add(child);
        }
    }

    private void Start()
    {
        AllSetup();
    }

    public void AllSetup()
    {
        gList[0].DOAnchorPosY(670, 0.4f);
        if (!_isFirst)
            gList[1].DOAnchorPosY(750, 0.4f);
        gList[2].DOAnchorPosX(405, 0.4f);
        gList[6].DOAnchorPosY(-650, 0.4f);
        gList[7].DOAnchorPosY(600, 0.4f);
        gList[8].DOAnchorPosX(1100, 0.4f);
    }

    public void UnAllSetup()
    {
        if (!_isFirst)
            firstText.SetActive(false);
        _isFirst = true;
        gList[0].DOAnchorPosY(-10, 0.4f);
        gList[1].DOAnchorPosY(444, 0.4f);
        gList[2].DOAnchorPosX(-10, 0.4f);
        gList[6].DOAnchorPosY(-85, 0.4f);
        gList[7].DOAnchorPosY(499, 0.4f);
        gList[8].DOAnchorPosX(880, 0.4f);
    }

}