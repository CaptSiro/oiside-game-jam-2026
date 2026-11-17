using Player;
using ScriptableObjects.EntityProjectiles;
using UnityEngine;

namespace Environment.EntityCombat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D collider;
        [SerializeField] private Rigidbody2D rigidbody2D;
        
        private ProjectileSO projectileSO;
        private PlayerMainScript playerRef;

        public void Setup(ProjectileSO proj, PlayerMainScript player)
        {
            projectileSO = proj;
            playerRef = player;
            
            spriteRenderer.sprite = proj.image;
            collider.size = spriteRenderer.sprite.bounds.size;
            rigidbody2D.linearVelocity = proj.speed * transform.up;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
}
}
