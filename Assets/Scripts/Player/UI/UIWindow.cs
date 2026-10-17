using UnityEngine;
using UnityEngine.Events;

namespace Player.UI
{
    public class UIWindow : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        private bool isActive = false;
        
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
