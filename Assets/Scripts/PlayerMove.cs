using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem ps;
    public Rigidbody rb;
    public GameObject player;

    //private PlayerInputActions playerInputs;
    private InputAction playerControls;

    private Skill Skill1;
    private Skill Skill2;
    private Skill Skill3;
    private Skill Skill4;

    public Vector3 Cam1;
    public Vector3 Cam2;
    public Vector3 Cam3;
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;

    private float intDamageMult;
    private float waitForHold;
    private GameLogic gameLogic;

    private float dash = 1f;
    private float dashCd = 1f;
    private bool shootPs = false;
    Vector2 moveDir = Vector2.zero;

    // Set player stats
    public void setStats(PlayerStats stats)
    {
        intDamageMult = stats.GetTotalInt()/12f;
    }

    void Start()
    {
        vcam = FindAnyObjectByType<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        gameLogic = FindAnyObjectByType<GameLogic>();
        Skill1 = Mage1st.IceStrike();
        Skill2 = Mage1st.ChainLightning();
        Skill3 = Mage1st.Explosion();
        Skill4 = Mage1st.FireBall();
        playerControls = InputSystem.actions.FindAction("Move");
    }

    // Assign inputs for player
    public void OnPlayerSkillE()
    {
        useInstantSkill(Skill1 as InstantSkill);
    }

    public void OnPlayerSkillF()
    {
        useProjectileSkill(Skill2 as ProjectileSkill);
    }

    public void OnPlayerSkillR()
    {
        useArcSkill(Skill3 as ArcSkill);
    }
    public void OnPlayerSkillG()
    {
        useProjectileSkill(Skill4 as ProjectileSkill);
    }

    public void OnInventoryButton()
    {
        gameLogic.toggleWindow(WindowType.Inventory);
    }

    public void OnEquipButton()
    {
        gameLogic.toggleWindow(WindowType.Equipment);
    }

    public void OnDashPress()
    {
        if (dashCd < 0.1f)
        {
            shootPs = true;
            dash = 1.85f;
            dashCd = 1.5f;
            waitForHold = 0.75f;
        }
        else if (dash < 1.1f)
        {
            dash = 1.1f;
        }
    }

    public void OnDashHold()
    {
        if (waitForHold > 0)
        {
            dash = 2.75f;
            dashCd = 3f;
        }
        else if (dash < 1.15f)
        {
            dash = 1.15f;
        }
    }

    public void equipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                {
                    var temppi = Instantiate(item.itemObject);
                    var wepPoint = transform.Find("Arm").Find("WeaponPoint");
                    temppi.transform.SetParent(wepPoint);
                    temppi.transform.SetPositionAndRotation(wepPoint.position, wepPoint.rotation);
                    //temppi.transform.position=transform.Find("Arm").Find("WeaponPoint").position;
                    //temppi.transform.rotation = transform.Find("Arm").Find("WeaponPoint").rotation;
                    return;
                }
            case ItemType.Hat:
                {
                    var temppi = Instantiate(item.itemObject);
                    var wepPoint = transform.Find("HatPoint");
                    temppi.transform.SetParent(wepPoint);
                    temppi.transform.SetPositionAndRotation(wepPoint.position, wepPoint.rotation);
                    return;
                }
            default: return;
        }
    }

    // Hard limit for camera coming too close
    public void camerafov(float fov)
    {
        FindAnyObjectByType<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov > 18f ? fov : 18f;
    }

    public Transform getRandomTarget(Vector3 location, float radius, bool random, List<Transform> ignore)
    {
        Transform target = null;
        float closestDist = 100000f;

        List<EnemyStats> enemyList = new List<EnemyStats>();
        var tempEnemyList = Physics.OverlapSphere(location, radius, LayerMask.GetMask("Enemy"));
        tempEnemyList = tempEnemyList.Where(x => x.transform.TryGetComponent<EnemyStats>(out _)).ToArray();

        if (tempEnemyList.Length > 0)
        {
            foreach (var foundEnemy in tempEnemyList)
            {
                if (!ignore.Contains(foundEnemy.transform))
                {
                    enemyList.Add(foundEnemy.transform.GetComponent<EnemyStats>());
                }
            }
        }

        if (enemyList == null || enemyList.Count == 0)
        {
            return null;
        }

        if (random)
        {
            return enemyList[Random.Range(0, enemyList.Count - 1)].transform;
        }

        foreach (var enem in enemyList)
        {
            if (enem.CompareTag("Enemy"))
                try
                {
                    Vector3 dirr = enem.transform.position - location;
                    float dist = dirr.sqrMagnitude;
                    if (dist < closestDist)
                    {
                        target = enem.transform;
                        closestDist = dist;
                    }
                }
                catch
                {
                    Destroy(this);
                }
        }

        return target;
    }

    public Transform getClosestTarget(Vector3 location)
    {
        Transform target = null;
        float closestDist = 100000f;

        List<EnemyStats> enemyList = new List<EnemyStats>();
        var tempEnemyList = Physics.OverlapSphere(location, 8f, LayerMask.GetMask("Enemy"));
        tempEnemyList = tempEnemyList.Where(x => x.transform.TryGetComponent<EnemyStats>(out _)).ToArray();

        if (tempEnemyList.Length > 0)
        {
            foreach (var foundEnemy in tempEnemyList)
            {
                enemyList.Add(foundEnemy.transform.GetComponent<EnemyStats>());
            }
        }
        else
        {
            tempEnemyList = Physics.OverlapSphere(location, 16f, LayerMask.GetMask("Enemy"));
            tempEnemyList = tempEnemyList.Where(x => x.transform.TryGetComponent<EnemyStats>(out _)).ToArray();
            if (tempEnemyList.Length > 0)
            {
                foreach (var foundEnemy in tempEnemyList)
                {
                    enemyList.Add(foundEnemy.transform.GetComponent<EnemyStats>());
                }
            }
        }

        if (enemyList == null || enemyList.Count == 0)
        {
            return null;
        }

        foreach (var enem in enemyList)
        {
            if (enem.CompareTag("Enemy"))
                try
                {
                    Vector3 dirr = enem.transform.position - location;
                    float dist = dirr.sqrMagnitude;
                    if (dist < closestDist)
                    {
                        target = enem.transform;
                        closestDist = dist;
                    }
                }
                catch
                {
                    Destroy(this);
                }
        }

        return target;
    }

   

    private void useArcSkill(ArcSkill arcSkill)
    {
        if (!gameLogic.useSkill(arcSkill.name)) return;
        if (arcSkill.attackArc == 360)
        {
            // change to instant skill?
            GameObject arcExplosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Skills/Mage/Explosion.prefab", typeof(GameObject));
            var explosionObject = Instantiate(arcExplosion, transform.position, Quaternion.identity);
            explosionObject.GetComponent<ExplosionCheck>().setDmg(arcSkill.damage * intDamageMult, arcSkill.attackRange);
        }
    }

    private void useInstantSkill(InstantSkill skill)
    {
        if (!gameLogic.useSkill(skill.name)) return;
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out var place);
        Vector3 targetVector = ray.GetPoint(place);
        GameObject chainBullet = Instantiate(skill.projectileGameObject, targetVector, Quaternion.identity);
        chainBullet.GetComponent<ExplosionCheck>().setDmg(skill.baseDamage * intDamageMult, 20f, skill.damageDelay);
    }

    private void useProjectileSkill(ProjectileSkill skill)
    {
        if (!gameLogic.useSkill(skill.name)) return;
        var po = skill.po;
        Transform target = null;
        Vector3 targetVector = new Vector3(0, 0);
        if (po.projectileType == ProjectileType.Tracking)
        {
            target = getClosestTarget(transform.position);
            if (target == null)
            {
                return;
            }
        }

        if (po.projectileType == ProjectileType.Targeted || po.projectileType == ProjectileType.Pointed || po.projectileType == ProjectileType.Chain)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            plane.Raycast(ray, out var place);
            targetVector = ray.GetPoint(place);
        }


        if (po.projectileType == ProjectileType.Chain)
        {
            target = getClosestTarget(targetVector);
            List<Transform> enemiesHit = new List<Transform>();
            if (target != null)
            {
                enemiesHit.Add(target);
            }
            if (enemiesHit.Count > 0)
            {
                for (int i = 0; i < po.pierceCount; i++)
                {
                    Transform tempEnemy = getRandomTarget(enemiesHit.Last().position, po.range, true, enemiesHit);
                    if (tempEnemy != null)
                    {
                        enemiesHit.Add(tempEnemy);
                    }
                }
                GameObject chainBullet = Instantiate(po.projectileGameObject, enemiesHit.First().position, transform.rotation);
                ProjectileBasic chainStats = chainBullet.GetComponent<ProjectileBasic>();
                po.damage = po.baseDamage * intDamageMult;
                chainStats.setProjectile(po, enemiesHit);
            }
            return;
        }



        po.damage = Mathf.RoundToInt(po.baseDamage * intDamageMult);
        GameObject bulletToShoot = Instantiate(po.projectileGameObject, gameObject.transform.position, gameObject.transform.rotation);
        ProjectileBasic projectileStats = bulletToShoot.GetComponent<ProjectileBasic>();
        projectileStats.setProjectile(po);
        if (po.projectileType == ProjectileType.Targeted || po.projectileType == ProjectileType.Pointed)
        {
            projectileStats.Seek(targetVector);
        }
        else if (po.projectileType == ProjectileType.Directional)
        {
            projectileStats.Seek(transform);
        }
        else projectileStats.Seek(target.transform);
    }

   

    private void Update()
    {
        if (Input.GetKeyDown("7"))
        {
            transposer.m_FollowOffset = Cam1;
        }
        if (Input.GetKeyDown("8"))
        {
            transposer.m_FollowOffset = Cam2;
        }
        if (Input.GetKeyDown("9"))
        {
            transposer.m_FollowOffset = Cam3;
        }

        moveDir = playerControls.ReadValue<Vector2>();
        float dashFinal = dash > 1f ? dash : 1f;
        rb.linearVelocity = new Vector3(moveDir.x * speed * dashFinal, rb.linearVelocity.y, moveDir.y * speed * dashFinal);

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out var place);

        Vector3 targetVector = ray.GetPoint(place);
        rb.transform.LookAt(new Vector3(targetVector.x, rb.transform.position.y, targetVector.z));

        // Check for dash cooldown effect

        if (dashCd > -0.1f)
        {
            dashCd -= (1 * Time.deltaTime);
            waitForHold -= (1 * Time.deltaTime);
            if (dashCd < 0.05f)
            {
                if (shootPs)
                {
                    shootPs = false;
                    ps.Emit(60);
                }
            }
        }

        // Slow down dash gradually
        if (dash > -0.1f)
        {
            dash -= (1 * Time.deltaTime);
        }

        // Movement
        

    }
}
