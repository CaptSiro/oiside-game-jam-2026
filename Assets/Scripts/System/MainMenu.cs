using UnityEngine;
using UnityEngine.SceneManagement;

namespace System
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
