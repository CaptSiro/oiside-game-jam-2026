using UnityEngine;

namespace Player.UI
{
    public class UIController : MonoBehaviour
    {
        
        [SerializeField] private UIWindow[] UIWindows;
        [SerializeField] private GameObject endScreen;

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
        
        
    }
}
