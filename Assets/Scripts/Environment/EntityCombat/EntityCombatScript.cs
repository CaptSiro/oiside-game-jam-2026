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
                        yield return SpawnProjectileAttack(entity.bulletHellDefinition.projectileAttacks[attack]);
                    }

                    break;
            }
            
        }

        private IEnumerator SpawnProjectileAttack(EntitySO.ProjectileAttack attack)
        {
            switch (attack.type)
            {
                case EntitySO.ShootingType.directed:
                    SpawnDirectedAttack(attack);
                    break;
                
                case EntitySO.ShootingType.around:
                    SpawnAroundAttack(attack);
                    break;
                
                case EntitySO.ShootingType.circleSequence:
                    float totalAngle = attack.amount * ANGLESTEP;

                    for (int i = 0; i < 360f / totalAngle; i++)
                    {
                        Vector2 pseudoTarget = PositionFromAngle(totalAngle * i);
                        yield return new WaitForSeconds(0.5f);
                        SpawnDirectedAttack(attack, pseudoTarget);
                    }

                    break;
            }
        }


        private Vector2 PositionFromAngle(float angle)
        {
            float radians = angle * Mathf.Deg2Rad;
            
            Vector2 position = new Vector2(transform.position.x, transform.position.y) + new Vector2(
                Mathf.Cos(radians),
                Mathf.Sin(radians)
            ) * SPAWNRADIUS;

            return position;
        }

        private void SpawnDirectedAttack(EntitySO.ProjectileAttack attack, Vector3 targetPosition = default(Vector3))
        {
            if (targetPosition == default(Vector3))
            {
                targetPosition = playerRef.transform.position;
            }
            Vector2 direction = targetPosition - transform.position;

            float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            float startAngle = centerAngle - (attack.amount * ANGLESTEP) / 2f;

            for (int i = 0; i < attack.amount; i++)
            {
                float angle = startAngle + i * ANGLESTEP;

                Vector2 spawnPosition = PositionFromAngle(angle);

                Projectile p = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, angle - 90f));
                p.Setup(attack.projectile, playerRef);
            }
        }

        private void SpawnAroundAttack(EntitySO.ProjectileAttack attack)
        {
            float angleStep = 360f / attack.amount;
            
            for (int i = 0; i < attack.amount; i++)
            {
                Vector2 spawnPosition = PositionFromAngle(angleStep * i);
                
                Projectile p = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, (angleStep * i) - 90f));
                p.Setup(attack.projectile, playerRef);
            }
        }
    }
}
