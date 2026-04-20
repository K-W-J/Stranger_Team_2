using UnityEngine;

public class DisableObj : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
