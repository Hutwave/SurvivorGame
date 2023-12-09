using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem ps;
    public Rigidbody rb;
    public GameObject player;
    public GameObject energyBolt;

    private PlayerInputActions playerInputs;
    private InputAction playerControls;
    private InputAction playerDashPress;
    private InputAction playerDashHold;

    private InputAction playerSkillE;
    private InputAction playerSkillF;
    private InputAction playerSkillR;
    private InputAction playerSkillG;

    private ProjectileObject Skill1;
    private ProjectileObject Skill2;
    private Skill Skill3;
    private ProjectileObject Skill4;

    public Vector3 Cam1;
    public Vector3 Cam2;
    public Vector3 Cam3;
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;

    private float waitForHold;
    private GameLogic gameLogic;
    private PlayerStats playerStats;

    private float dash = 1f;
    private float dashCd = 1f;

    private bool shootPs = false;
    Vector2 moveDir = Vector2.zero;

    // Set player stats
    public void setStats(float dash, float dashcd, float speed)
    {
        waitForHold = 1.5f;
    }

    void Start()
    {
        vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        gameLogic = FindAnyObjectByType<GameLogic>();
        Skill1 = Mage1st.EnergyBolt();
        Skill2 = Mage1st.ChainLightning();
        Skill3 = Mage1st.Explosion();
        Skill4 = Mage1st.FireBall();

        playerStats = new PlayerStats();
        playerStats.InitializeNewPlayer(PlayerClass.Magician);
    }

    // Assign inputs for player
    private void Awake()
    {
        playerInputs = new PlayerInputActions();
    }

    // Assign controls on player spawn
    private void OnEnable()
    {
        playerControls = playerInputs.Player.Move;
        playerDashPress = playerInputs.Player.DashPress;
        playerDashHold = playerInputs.Player.DashHold;

        playerSkillE = playerInputs.Player.SkillE;
        playerSkillF = playerInputs.Player.SkillF;
        playerSkillR = playerInputs.Player.SkillR;
        playerSkillG = playerInputs.Player.SkillG;

        playerDashPress.performed += PlayerDashPress_performed;
        playerDashHold.performed += PlayerDashHold_performed;
        playerSkillE.performed += PlayerSkillE_performed;
        playerSkillR.performed += PlayerSkillR_performed;
        playerSkillG.performed += PlayerSkillG_performed;
        playerSkillF.performed += PlayerSkillF_performed;

        playerControls.Enable();
        playerDashPress.Enable();
        playerDashHold.Enable();
        playerSkillE.Enable();
        playerSkillF.Enable();
        playerSkillR.Enable();
        playerSkillG.Enable();
    }

    // Give player a light when field gets too dim
    public void lightsOn()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Hard limit for camera coming too close
    public void camerafov(float fov)
    {
        FindObjectOfType<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov > 18f ? fov : 18f;
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

    private void PlayerDashPress_performed(InputAction.CallbackContext obj)
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

    private void PlayerDashHold_performed(InputAction.CallbackContext obj)
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

    private void useArcSkill(ArcSkill arcSkill)
    {
        if(arcSkill.attackArc == 360)
        {
            
            GameObject arcExplosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Skills/Mage/Explosion.prefab", typeof(GameObject));
            var explosionObject = Instantiate(arcExplosion, transform.position, Quaternion.identity);
            explosionObject.GetComponent<ExplosionCheck>().setDmg(arcSkill.damage, arcSkill.attackRange);
        }
    }

    private void useProjectileSkill(ProjectileObject po)
    {
        Transform target = null;
        Vector3 targetVector = new Vector3(0, 0);
        Debug.LogError(po.projectileType);
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
            Debug.LogError("AA!!!!!!");
            List<Transform> enemiesHit = new List<Transform>();
            if(target != null)
            {
                enemiesHit.Add(target);
            }
            Debug.LogError("BB " + enemiesHit.Count);
            for (int i = 0; i < po.pierceCount; i++)
            {
                Transform tempEnemy = getRandomTarget(enemiesHit.Last().position, po.range, true, enemiesHit);
                if(tempEnemy != null)
                {
                    enemiesHit.Add(tempEnemy);
                }
            }
            Debug.LogError("AA " + enemiesHit.Count);
            /*enemiesHit.ForEach(oneEnemy =>
            {
                oneEnemy.GetComponent<EnemyStats>().takeDamage(Mathf.RoundToInt(po.damage));
            });
            */
            GameObject chainBullet = Instantiate(po.projectileGameObject, enemiesHit.First().position, transform.rotation);
            ProjectileBasic chainStats = chainBullet.GetComponent<ProjectileBasic>();
            chainStats.setProjectile(po, enemiesHit);
            return;
        }



        po.damage = Mathf.RoundToInt(po.baseDamage + playerStats.GetTotalInt() + playerStats.GetTotalAtt());
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

    private void PlayerSkillE_performed(InputAction.CallbackContext obj)
    {
        useProjectileSkill(Skill1);
    }

    private void PlayerSkillF_performed(InputAction.CallbackContext obj)
    {
        useProjectileSkill(Skill2);
    }

    private void PlayerSkillR_performed(InputAction.CallbackContext obj)
    {
        useArcSkill(Skill3 as ArcSkill);
    }

    private void PlayerSkillG_performed(InputAction.CallbackContext obj)
    {
        useProjectileSkill(Skill4);
    }

    private void Update()
    {
        if (Input.GetKeyDown("7")){
           transposer.m_FollowOffset = Cam1;
        }
        if (Input.GetKeyDown("8")){
           transposer.m_FollowOffset = Cam2;
        }
        if (Input.GetKeyDown("9")){
           transposer.m_FollowOffset = Cam3;
        }
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
        moveDir = playerControls.ReadValue<Vector2>();
        float dashFinal = dash > 1f ? dash : 1f;
        rb.velocity = new Vector3(moveDir.x * speed * dashFinal, rb.velocity.y, moveDir.y * speed * dashFinal);
        if (rb.velocity.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(rb.velocity, transform.up);
            rotation.x = 0;
            rotation.z = 0;
            rb.rotation = rotation.normalized;
        }
    }
}
