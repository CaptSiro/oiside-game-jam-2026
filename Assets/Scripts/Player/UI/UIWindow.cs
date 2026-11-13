using UnityEngine;
using UnityEngine.Events;

namespace Player.UI
{
    public class UIWindow : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        protected bool isActive = false;
        protected UIController uiController;

        public void SetController(UIController controller)
        {
            uiController = controller;
        }
        
        public virtual void ToggleWindow()
        {
            if (isActive)
            {
                onClose?.Invoke();
            }
            else
            {
                onOpen?.Invoke();
            }
            isActive = !isActive;
        }
    }
}
