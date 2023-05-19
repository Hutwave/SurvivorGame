using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public ParticleSystem ps;
    public Rigidbody rb;
    public GameObject player;

    private PlayerInputActions playerInputs;
    private InputAction playerControls;
    private InputAction playerDashPress;
    private InputAction playerDashHold;
    
    private float levelUpDash;
    private float levelUpDashCd;
    private float levelUpSpeed;
    private float waitForHold;

    private bool canDie;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    

    private float dash = 1f;
    private float dashCd = 1f;

    private bool shootPs = false;
    Vector2 moveDir = Vector2.zero;

    // Set player stats
    public void setStats(float dash, float dashcd, float speed)
    {
        levelUpDash = dash;
        levelUpDashCd = dashcd;
        levelUpSpeed = speed;
        waitForHold = 1.5f;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    //public void setHealth(float health)
    //{
    //    playerHealth = health;
    //}

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        gameOver();
    }

     void OnCollisionEnter(Collision collision)
     {
        try
        {
            if (collision.collider.gameObject.CompareTag("Enemy"))
            {
                takeDamage(collision.collider.gameObject.GetComponent<EnemyStats>().enemyDamage);
            }
        }

        catch
        {
            // Name of object which is enemy but no enemyStats
            Debug.LogWarning(collision.collider.gameObject.name);
        }
     }  

    public void cantDie()
    {
        canDie = false;
    }

    private void gameOver()
    {
        if (canDie && currentHealth < 0f)
        {
            // Game over
        }
    }

    // Assign inputs for player
    private void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        playerInputs = new PlayerInputActions();
        canDie = true;   
    }

    // Assign controls on player spawn
    private void OnEnable()
    {
        playerControls = playerInputs.Player.Move;
        playerDashPress = playerInputs.Player.DashPress;
        playerDashHold = playerInputs.Player.DashHold;

        playerDashPress.performed += PlayerDashPress_performed;
        playerDashHold.performed += PlayerDashHold_performed;

        playerControls.Enable();
        playerDashPress.Enable();
        playerDashHold.Enable();        
    }

    // Give player a light when field gets too dim
    public void lightsOn()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Hard limit for camera coming too close
    public void camerafov(float fov)
    {
        FindObjectOfType<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov > 18f ? fov : 18f;
    }

    private void PlayerDashPress_performed(InputAction.CallbackContext obj)
    {
        float actualLevelCd = (1f - (1f - levelUpDashCd));
        if (dashCd < 0.1f)
        {
            shootPs = true;
            dash = 1.85f * (1.2f * levelUpDash);
            dashCd = 1.5f * actualLevelCd;
            waitForHold = 0.75f;
        }
        else if (dash < 1.1f)
        {
            dash = 1.1f;
        }
    }

    private void PlayerDashHold_performed(InputAction.CallbackContext obj)
    {
        float actualLevelCd = (1f - (1f - levelUpDashCd));
        if (waitForHold > 0)
        {
            dash = 2.75f * levelUpDash;
            dashCd = 3f * (1.5f * actualLevelCd);
        }
        else if (dash < 1.15f)
        {
            dash = 1.15f;
        }
    }

    private void Update()
    { 
        // Check for dash cooldown effect

        if(dashCd > -0.1f)
        {
            dashCd -= (1* Time.deltaTime);
            waitForHold -= (1* Time.deltaTime);
            if(dashCd < 0.05f)
            {
                if (shootPs)
                {
                    shootPs = false;
                    ps.Emit(60);
                }
            }
        }

        // Slow down dash gradually
        if(dash > -0.1f)
        {
            dash -= (1 * Time.deltaTime);
        }

        // Movement
        moveDir = playerControls.ReadValue<Vector2>();

        rb.velocity = new Vector3(moveDir.x * speed * levelUpSpeed * (dash > 1f ? dash : 1), rb.velocity.y, moveDir.y * speed * (dash > 1f ? dash : 1));
        if(rb.velocity.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(rb.velocity, transform.up);
            rotation.x = 0;
            rotation.z = 0;
            if(Mathf.Abs(rotation.y) > 0.01f) {
                try
                {
                    rb.rotation = rotation;
                }
                catch (System.Exception e)
                {
                    rb.rotation = Quaternion.identity;
                    Debug.LogWarning(e.Message);
                }                
            }
        }
    }
}
