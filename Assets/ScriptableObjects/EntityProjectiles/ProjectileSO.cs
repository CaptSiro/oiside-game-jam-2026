using UnityEngine;

namespace ScriptableObjects.EntityProjectiles
{
    [CreateAssetMenu(fileName = "ProjectileSO", menuName = "Scriptable Objects/ProjectileSO")]
    public class ProjectileSO : ScriptableObject
    {
        public Sprite image;
        public float damage;
        public float speed;
        
        public bool isGuided;
    }
}
