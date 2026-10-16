using UnityEngine;

namespace Player.UI
{
    public class UIController : MonoBehaviour
    {
        
        [SerializeField] private UIWindow[] UIWindows;

        private int currentWindow;

        public void OpenWindow(int windowType)
        {
            if (currentWindow != -1 && currentWindow != windowType)
            {
                UIWindows[currentWindow].ToggleWindow();
            }    
            
            currentWindow = windowType;
            UIWindows[currentWindow].ToggleWindow();
        }

        public void CloseWindow(int windowType)
        {
            if (currentWindow == windowType)
            {
                UIWindows[currentWindow].ToggleWindow();
                currentWindow = -1;
            }
        }
        
        
    }
}
