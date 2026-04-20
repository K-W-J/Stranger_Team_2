using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01_Work.JY._01_Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("GameScene"); 
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
