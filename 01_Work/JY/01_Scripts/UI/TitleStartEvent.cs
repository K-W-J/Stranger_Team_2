using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class TitleStartEvent : MonoBehaviour
{
    [SerializeField] private GameObject fadeObj;
    private Image _fadeImage;

    private void Awake()
    {
        _fadeImage = fadeObj.GetComponent<Image>();
        fadeObj.SetActive(false);
    }

    public void StartGame()
    {
        fadeObj.SetActive(true);
        _fadeImage.DOFade(1, 0.7f).OnComplete(()
            => LoadScene()
        );
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
