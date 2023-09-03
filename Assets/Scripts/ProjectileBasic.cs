using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public enum ProjectileType
{
    Tracking,
    Targeted,
    Directional,
    Pointed
}

public class ProjectileBasic : MonoBehaviour
{
    internal ProjectileType projectileType;
    internal float speed = 10f;
    public GameObject explosion;
    internal bool isExplosive;
    internal bool piercing;
    internal int pierceAmount;
    internal float colliderSize;
    Collider[] hitEnemies;
    
    [SerializeReference]
    public ProjectileObject proj;
    

    internal Transform target;
    internal Vector3 targetVector;
    internal float damage;
    internal float explosionRadius;

    internal float range;
    internal float lifeTime;

    private int targetedHasDirection = 5;

    public void setProjectile(ProjectileObject obj)
    {
        this.explosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Skills/Small AoE.prefab", typeof(GameObject));

            projectileType = (ProjectileType)obj.projectileType;

            isExplosive = (bool)obj.isExplosive;

            piercing = (bool)obj.isPiercing;

            damage = (float)obj.damage;

            explosionRadius = (float)obj.explosionRadius;

            range = (float)obj.range;

            lifeTime = (float)obj.lifeTime;

            speed = (float)obj.speed;
        
    }

    internal void HitTarget()
    {
        if (isExplosive)
        {
            var explosionObject = Instantiate(explosion, transform.position, Quaternion.identity);
            explosionObject.GetComponent<ExplosionCheck>().setDmg(damage, explosionRadius);
        }
        
        Destroy(this.gameObject);
        return;
    }

    public void Seek(Vector3 _target)
    {
        targetVector = _target;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;
        switch (projectileType)
        {
            /// TARGETED /// 
            case ProjectileType.Targeted:
                // MAAN ALLE
                Vector3 targetDir = new Vector3(targetVector.x, 0.5f, targetVector.z) - transform.position;
                if (!isExplosive)
                {
                    targetDir = transform.forward;
                }
                else if (targetDir.magnitude <= distanceThisFrame)
                {
                    HitTarget();
                }
                Vector3 targetti = new Vector3(targetDir.normalized.x, 0f, targetDir.normalized.z);
                transform.Translate(targetti * distanceThisFrame, Space.World);
                shouldHit();
                break;

            /// TRACKING /// 
            case ProjectileType.Tracking:
                if (target == null)
                {
                    Destroy(gameObject);
                }

                Vector3 trackDir = target.transform.position - transform.position;
                if (trackDir.magnitude <= distanceThisFrame || trackDir.magnitude > 16f)
                {
                    HitTarget();
                }
                shouldHit();
                transform.Translate(trackDir.normalized * distanceThisFrame, Space.World);
                break;

            /// POINTED ///
            case ProjectileType.Pointed:
                // MAAN ALLE
                if (targetedHasDirection > 0)
                {
                    Vector3 pointDir = new Vector3(targetVector.x, 0.5f, targetVector.z) - transform.position;
                    targetedHasDirection--;
                    Vector3 targetti2 = new Vector3(pointDir.normalized.x, 0f, pointDir.normalized.z);
                    transform.Translate(pointDir.normalized * distanceThisFrame, Space.World);
                    transform.LookAt(targetVector, Vector3.up);
                    shouldHit();
                }
                else
                {
                    Vector3 pointDir = new Vector3(transform.forward.x, 0f, transform.forward.z);
                    transform.Translate(pointDir * speed * Time.deltaTime, Space.World);
                }
                shouldHit();
                break;

            /// DIRECTIONAL /// 
            case ProjectileType.Directional:
                Vector3 dir = transform.forward;
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
                shouldHit();
                break;
        }
    }

    private void shouldHit()
    {
        if (piercing)
        {
            Collider[] newEnemies = Physics.OverlapSphere(transform.position, colliderSize, LayerMask.GetMask("Enemy"));
            newEnemies = newEnemies.Except(hitEnemies).ToArray();
            foreach (var newEnemy in newEnemies)
            {
                newEnemy.transform.GetComponent<EnemyStats>().takeDamage(Mathf.RoundToInt(damage));
                pierceAmount--;
                if (!piercing || pierceAmount < 1)
                {
                    HitTarget();
                    break;
                }
            }

            hitEnemies = hitEnemies.Concat(newEnemies).ToArray();
        }

        else if(Physics.CheckSphere(transform.position, colliderSize, LayerMask.GetMask("Enemy")))
        {
            HitTarget();
        }
    }

    /*
   public void addStatus(statusEnums statusEnum, float amount)
   {
    private float poison = 0;
    private float slow = 0;
    private float fire = 0;
    private float weakened = 0;
    private float regen = 0;
    private float hardened = 0;

       switch (statusEnum)
       {
           case statusEnums.Poison:
               poison = amount;
               break;
           case statusEnums.Fire:
               fire = amount;
               break;
           case statusEnums.Slow:
               slow = amount;
               break;
           case statusEnums.Weakened:
               weakened = amount;
               break;
           case statusEnums.Regen:
               regen = amount;
               break;
           case statusEnums.Hardened:
               hardened = amount;
               break;
           default:
               return;
       }
   }
   */
}
