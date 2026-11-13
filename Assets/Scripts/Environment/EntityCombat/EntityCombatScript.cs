using System.Collections;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Environment.EntityCombat
{
    public class EntityCombatScript : MonoBehaviour
    {
        private EntitySO entity;
        private Projectile projectilePrefab;

        public void StartCombat(EntitySO e, Player)
        {
            projectilePrefab = Resources.Load<Projectile>("Combat/ProjectilePrefab");
            entity = e;
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
                    
            }
        }
    }
}
