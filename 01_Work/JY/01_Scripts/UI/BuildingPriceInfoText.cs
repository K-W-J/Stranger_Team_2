using _01_Work.HS.Core;
using TMPro;
using UnityEngine;

public class BuildingPriceInfoText : MonoSingleton<BuildingPriceInfoText>
{
    [SerializeField] private GameObject priceText;

    private RectTransform _pPos;
    public TMP_Text canText { get; set; }

    private void Awake()
    {
        _pPos = priceText.GetComponent<RectTransform>();
        canText = priceText.transform.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ClosePanels();
    }

    public void ShowPriceText(GameObject obj, string objName, int woodPrice, int stonePrice, int crystalPrice, int goldPrice, int foodPrice, string description)
    {
        priceText.SetActive(true);
        TMP_Text text = priceText.transform.GetChild(0).GetComponent<TMP_Text>();

        text.text = "";
        text.text += $"{objName} \n";
        if (goldPrice != 0)
        {
            text.text += $"\n골드 : {goldPrice} \n";
        }
        if (woodPrice != 0)
        {
            text.text += $"나무 : {woodPrice} \n";
        }
        if (stonePrice != 0)
        {
            text.text += $"돌 : {stonePrice} \n";
        }
        if (crystalPrice != 0)
        {
            text.text += $"크리스탈 : {crystalPrice} \n";
        }
        if (foodPrice != 0)
        {
            text.text += $"음식 : {foodPrice} \n";
        }
        
        text.text += $"\n{description}";
        

        Vector3 pos = obj.transform.position;
        pos.y += 60;
        _pPos.transform.position = pos;
    }

    public void ClosePanels()
    {
        priceText.SetActive(false);
    }
}
