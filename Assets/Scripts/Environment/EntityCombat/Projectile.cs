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

        public void Setup(ProjectileSO proj)
        {
            this.projectileSO = proj;
            spriteRenderer.sprite = proj.image;
            collider.size = spriteRenderer.sprite.bounds.size;
            rigidbody2D.linearVelocity = proj.speed * transform.up;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
        }
}
}
