using UnityEngine;

public class TraderItem : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private int price;
    [SerializeField] private int count;

    private ResourceManager _resource;

    private void Awake()
    {
        _resource = ResourceManager.Instance;
    }

    public void BuyItems()
    {
        if (_resource.CheckCanGoldUse(price) && _resource.GetCurResource(type) <= _resource.GetCurMaxResource(type))
        {
            _resource.UseResource(ResourceType.GOLD, price);
            _resource.AddResorce(type, 100);
        }
    }
}
