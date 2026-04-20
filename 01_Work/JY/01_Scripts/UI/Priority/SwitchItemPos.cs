using _01_Work.KWJ._01_Scripes.WorkingUnit;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchItemPos : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private float moveDuration = 0.2f;
    [field: SerializeField] public TextMeshProUGUI Text { get; set; } // �̵� �ִϸ��̼� �ð�

    public WorkType worktype { get; set; }

    private RectTransform content;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (upButton != null)
            upButton.onClick.AddListener(MoveUp);
        if (downButton != null)
            downButton.onClick.AddListener(MoveDown);
    }

    private void Start()
    {
        rectTransform.localScale  = new Vector2(1, 1);
            
        Transform parent = transform.parent;
        Debug.Log(parent.name);
        if (parent != null && parent is RectTransform)
        {
            content = parent as RectTransform;
        }


        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upButton.gameObject.SetActive(true);
        downButton.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upButton.gameObject.SetActive(false);
        downButton.gameObject.SetActive(false);
    }

    private void MoveUp()
    {
        int index = transform.GetSiblingIndex();
        if (index > 0)
        {
            WorkingUnitManager.Instance.UpPriorityUI(worktype);
            Transform prev = content.GetChild(index - 1);
            Vector3 targetPos = prev.localPosition;

            prev.DOLocalMove(rectTransform.localPosition, moveDuration);
            rectTransform.DOLocalMove(targetPos, moveDuration).OnComplete(() =>
            {
                transform.SetSiblingIndex(index - 1);
            });
        }
    }

    private void MoveDown()
    {
        int index = transform.GetSiblingIndex();
        int siblingCount = content.childCount;
        if (index < siblingCount - 1)
        {
            WorkingUnitManager.Instance.DownPriorityUI(worktype);
            Transform next = content.GetChild(index + 1);
            Vector3 targetPos = next.localPosition;

            next.DOLocalMove(rectTransform.localPosition, moveDuration);
            rectTransform.DOLocalMove(targetPos, moveDuration).OnComplete(() =>
            {
                transform.SetSiblingIndex(index + 1);
            });
        }
    }
}
