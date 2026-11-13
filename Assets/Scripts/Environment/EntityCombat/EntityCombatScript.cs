using System.Collections;
using Player;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Environment.EntityCombat
{
    public class EntityCombatScript : MonoBehaviour
    {
        private const float ANGLESTEP = 4.5f, SPAWNRADIUS = 1f;
        
        private EntitySO entity;
        private Projectile projectilePrefab;
        private PlayerMainScript playerRef;

        public void StartCombat(EntitySO e, PlayerMainScript player)
        {
            projectilePrefab = Resources.Load<Projectile>("Combat/ProjectilePrefab");
            entity = e;
            playerRef = player;
            StartCoroutine(CombatCoroutine());
        }

        private IEnumerator CombatCoroutine()
        {
            switch (entity.combatType)
            {
                case EntitySO.CombatType.BulletHell:
                    
                    while (gameObject.activeSelf)
                    {
                        yield return new WaitForSeconds(entity.bulletHellDefinition.attackDelay == 0 ? 1f : entity.bulletHellDefinition.attackDelay);
                        int attack = Random.Range(0, entity.bulletHellDefinition.projectileAttacks.Length);
                        SpawnProjectileAttack(entity.bulletHellDefinition.projectileAttacks[attack]);
                    }

                    break;
            }
            
        }

        private void SpawnProjectileAttack(EntitySO.ProjectileAttack attack)
        {
            switch (attack.type)
            {
                case EntitySO.ShootingType.directed:
                    SpawnDirectedAttack(attack);
                    break;
            }
        }

        private void SpawnDirectedAttack(EntitySO.ProjectileAttack attack)
        {
            Vector2 direction = playerRef.transform.position - transform.position;

            float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            float startAngle = centerAngle - (attack.amount * ANGLESTEP) / 2f;

            for (int i = 0; i < attack.amount; i++)
            {
                float angle = startAngle + i * ANGLESTEP;

                float radians = angle * Mathf.Deg2Rad;

                Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(
                    Mathf.Cos(radians),
                    Mathf.Sin(radians)
                ) * SPAWNRADIUS;

                Projectile p = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, angle - 90f));
                p.Setup(attack.bullet);
            }
        }
    }
}
