using ScriptableObjects.EntityProjectiles;
using UnityEngine;

namespace Environment.EntityCombat
{
    public class Projectile : MonoBehaviour
    {
        private ProjectileSO projectileSO;

        public void Setup(ProjectileSO proj)
        {
            this.projectileSO = proj;
        }
}
}
