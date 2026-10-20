using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    ///Should be base class of any player script so that it can be retrieved from PlayerMainScript generically.
    /// </summary>
    [Serializable]
    public class PlayerBaseClass : MonoBehaviour
    {
        protected PlayerMainScript playerRef;

        public void SetPlayerMainScript(PlayerMainScript script)
        {
            this.playerRef = script;
        }
    }
    
    
    public class PlayerMainScript : MonoBehaviour
    {
        [SerializeField] private PlayerBaseClass[] scripts;
        
        [Space(10)]
        [Header("Other references")]
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].SetPlayerMainScript(this);
            }
        }

        public T GetScript<T>() where T : PlayerBaseClass
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                if (scripts[i].GetType() == typeof(T))
                {
                    return (T)scripts[i];
                }
            }

            return null;
        }

        public Rigidbody2D GetRigidbody()
        {
            return rb;
        }
    }
}
