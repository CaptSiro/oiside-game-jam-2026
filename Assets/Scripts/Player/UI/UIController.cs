using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.UI
{
    /// <summary>
    /// Handles opening and closing of UIwindow type objects and holds references so that individual scripts don't have to. Also hold references to non window parts of UI.
    /// </summary>
    public class UIController : PlayerBaseClass
    {
        
        [SerializeField] private UIWindow[] UIWindows;
        [SerializeField] private GameObject endScreen, pauseScreen;

        private int currentWindow = -1;

        public UIWindow OpenWindow(int windowType)
        {
            if (currentWindow != -1 && currentWindow != windowType)
            {
                UIWindows[currentWindow].ToggleWindow();
            }    
            
            currentWindow = windowType;
            UIWindows[currentWindow].ToggleWindow();
            if (currentWindow == -1)
            {
                return null;
            }
            
            return UIWindows[currentWindow];
        }

        public void OpenWindowButton(int windowType)
        {
            OpenWindow(windowType);
        }

        public void CloseWindow(int windowType)
        {
            if (currentWindow == windowType)
            {
                UIWindows[currentWindow].ToggleWindow();
                currentWindow = -1;
            }
        }

        public void ShowBook()
        {
            UIWindows[0].gameObject.SetActive(true);
        }

        public void ShowEndScreen()
        {
            endScreen.SetActive(true);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void TogglePause()
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
        
    }
}
