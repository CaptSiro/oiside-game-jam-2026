using UnityEngine;
using UnityEngine.Events;

namespace Player.UI
{
    public class UIWindow : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;
        
        public virtual void ToggleWindow()
        {
            if (gameObject.activeSelf)
            {
                onClose?.Invoke();
            }
            else
            {
                onOpen?.Invoke();
            }
            
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
