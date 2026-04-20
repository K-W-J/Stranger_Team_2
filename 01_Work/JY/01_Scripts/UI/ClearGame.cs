using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01_Work.JY._01_Scripts.UI
{
    public class ClearGame : MonoBehaviour
    {
        public void ReStart()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}