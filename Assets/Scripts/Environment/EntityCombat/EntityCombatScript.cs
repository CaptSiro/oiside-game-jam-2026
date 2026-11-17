using System;
using System.Collections;
using Player;
using Player.UI;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.EntityCombat
{
    public class EntityCombatScript : MonoBehaviour
    {
        private const float ANGLESTEP = 15f, SPAWNRADIUS = 1f;
        
        private EntitySO entity;
        private Projectile projectilePrefab;
        private PlayerMainScript playerRef;
        private Rigidbody2D rb2d;
        
        private bool isCharging = false;


        public void StartCombat(EntitySO e, PlayerMainScript player)
        {
            projectilePrefab = Resources.Load<Projectile>("Combat/ProjectilePrefab");
            entity = e;
            playerRef = player;
            playerRef.GetScript<UIController>().SetUpHealthBar(entity);
            
            
            StartCoroutine(CombatCoroutine());
        }

        private IEnumerator CombatCoroutine()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(entity.delayBetweenAttacks == 0
                    ? 1f
                    : entity.delayBetweenAttacks);
                int attack = Random.Range(0, entity.attacks.Length);

                yield return entity.attacks[attack].Execute(this);
            }

        }

        #region projectileAttack
        
        public IEnumerator SpawnProjectileAttack(EntitySO.ProjectileAttack attack)
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
                    float firstAngle = AngleToPosition(playerRef.transform.position, transform.position);

                    for (int i = 0; i < 360f / totalAngle; i++)
                    {
                        Vector2 pseudoTarget = PositionFromAngle(totalAngle * i + firstAngle, new Vector2(transform.position.x, transform.position.y));
                        yield return new WaitForSeconds(0.5f);
                        SpawnDirectedAttack(attack, pseudoTarget);
                    }

                    break;
            }
        }


        private Vector2 PositionFromAngle(float angle, Vector2 origin)
        {
            float radians = angle * Mathf.Deg2Rad;
            
            Vector2 position = origin + new Vector2(
                Mathf.Cos(radians),
                Mathf.Sin(radians)
            ) * SPAWNRADIUS;

            return position;
        }

        private float AngleToPosition(Vector3 targetPosition, Vector3 sourcePosition)
        {
            Vector2 direction = targetPosition - sourcePosition;

            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        private void SpawnDirectedAttack(EntitySO.ProjectileAttack attack, Vector3 targetPosition = default(Vector3))
        {
            if (targetPosition == default(Vector3))
            {
                targetPosition = playerRef.transform.position;
            }

            float centerAngle = AngleToPosition(targetPosition, transform.position);
            
            float startAngle = centerAngle - (attack.amount * ANGLESTEP) / 2f;

            for (int i = 0; i < attack.amount; i++)
            {
                float angle = startAngle + i * ANGLESTEP;

                Vector2 spawnPosition = PositionFromAngle(angle, new Vector2(transform.position.x, transform.position.y));

                Projectile p = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, angle - 90f));
                p.Setup(attack.projectile, playerRef);
            }
        }

        private void SpawnAroundAttack(EntitySO.ProjectileAttack attack)
        {
            float angleStep = 360f / attack.amount;
            
            for (int i = 0; i < attack.amount; i++)
            {
                Vector2 spawnPosition = PositionFromAngle(angleStep * i, new Vector2(transform.position.x, transform.position.y));
                
                Projectile p = Instantiate(projectilePrefab, spawnPosition, Quaternion.Euler(0, 0, (angleStep * i) - 90f));
                p.Setup(attack.projectile, playerRef);
            }
        }
        
        #endregion

        #region ChargeAttack

        public IEnumerator StartChargeAttack(EntitySO.ChargeAttack attack)
        {
            if (rb2d == null)
            {
                rb2d = transform.AddComponent<Rigidbody2D>();
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2d.gravityScale = 0f;
            }

            yield return new WaitForSeconds(0.5f);
            
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            Vector2 direction = playerRef.transform.position - transform.position;
            rb2d.linearVelocity = direction.normalized * attack.speed;
            isCharging = true;

            if (attack.maxDistance > 0)
            {
                yield return new WaitForSeconds(attack.maxDistance / attack.speed);
                rb2d.linearVelocity = Vector2.zero;
                isCharging = false;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                yield return new WaitWhile(() => isCharging);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!isCharging)return;
            
            if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                isCharging = false;
                rb2d.linearVelocity = Vector2.zero;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                //TODO deal damage to player
            }
        }

        #endregion
    }
}
