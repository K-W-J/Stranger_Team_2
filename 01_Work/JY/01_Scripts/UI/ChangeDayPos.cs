using _01_Work.HS.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDayPos : MonoSingleton<ChangeDayPos>
{
    [SerializeField] private Transform dayCircle;
    [SerializeField] private Transform nightCircle;

    public Image dayImage;
    public Image nightImage;

    private float _duration = 2f;
    private bool _isDay = false;

    public void ChangePos()
    {
        if (!_isDay)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(dayCircle.DOMoveX(nightCircle.position.x, _duration).SetEase(Ease.InOutBack));
            seq.Append(nightCircle.DOMoveX(dayCircle.position.x, _duration).SetEase(Ease.InOutBack));
        }
        else
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(nightCircle.DOMoveX(dayCircle.position.x, _duration).SetEase(Ease.InOutBack));
            seq.Append(dayCircle.DOMoveX(nightCircle.position.x, _duration).SetEase(Ease.InOutBack));
        }
        _isDay = !_isDay;
        //dayImage.fillAmount = 0;
        //nightImage.fillAmount = 0;
    }
}
