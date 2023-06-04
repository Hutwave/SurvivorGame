using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum ProjectileType
{
    Tracking,
    Targeted,
    Directional
}

public class ProjectileBasic : MonoBehaviour
{
    internal ProjectileType projectileType;
    internal float speed = 10f;
    public GameObject explosion;
    internal bool isExplosive;
    internal bool piercing;
    
    [SerializeReference]
    public ProjectileObject proj;
    

    internal Transform target;
    internal Vector3 targetVector;
    internal float damage;
    internal float explosionRadius;

    internal float range;
    internal float lifeTime;

    private bool targetedHasDirection;

    public void setProjectile(ProjectileObject obj)
    {
        this.explosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Skills/Small AoE.prefab", typeof(GameObject));
        if(obj.projectileType != null)
        {
            projectileType = (ProjectileType)obj.projectileType;
        }
        
        if(obj.isExplosive != null)
        {
            isExplosive = (bool)obj.isExplosive;
        }
        
        if(obj.isPiercing != null)
        {
            piercing = (bool)obj.isPiercing;
        }

        if (obj.damage != null)
        {
            damage = (float)obj.damage;
        }

        if (obj.explosionRadius != null)
        {
            explosionRadius = (float)obj.explosionRadius;
        }

        if (obj.range != null)
        {
            range = (float)obj.range;
        }

        if (obj.lifeTime != null)
        {
            lifeTime = (float)obj.lifeTime;
        }

        if (obj.speed != null)
        {
            speed = (float)obj.speed;
        }
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
                
                Vector3 targetDir = targetVector - transform.position;
                if (!isExplosive && targetedHasDirection)
                {
                    targetDir = transform.forward;
                }

                else if (targetDir.magnitude <= distanceThisFrame)
                {
                    HitTarget();
                }

                targetedHasDirection = false;
                transform.Translate(targetDir.normalized * distanceThisFrame, Space.World);
                break;

            /// TRACKING /// 
            case ProjectileType.Tracking:
                if (target == null)
                {
                    Destroy(gameObject);
                }

                if (target.gameObject.activeInHierarchy)
                {
                    HitTarget();
                }

                Vector3 trackDir = target.transform.position - transform.position;
                
                if (trackDir.magnitude <= distanceThisFrame)
                {
                    HitTarget();
                }
                transform.Translate(trackDir.normalized * distanceThisFrame, Space.World);
                break;

            /// DIRECTIONAL /// 
            case ProjectileType.Directional:
                Vector3 dir = transform.forward;
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
                break;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isExplosive)
            {
                collision.gameObject.transform.GetComponent<EnemyStats>().takeDamage(Mathf.RoundToInt(damage));
            }

            if (!piercing)
            {
                HitTarget();
            }
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
