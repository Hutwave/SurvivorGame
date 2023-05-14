using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public float speed = 5f;
    private InputAction playerControls;
    private InputAction playerDashPress;
    private InputAction playerDashHold;
    public Rigidbody rb;
    public GameObject player;

    float levelUpDash;
    float levelUpDashCd;
    float levelUpSpeed;
    private float actualLevelUpDashCd;


    public ParticleSystem ps;
    bool shootPs = false;

    bool canDie;

    Vector2 moveDir = Vector2.zero;
    float dash = 1f;
    float dashCd = 1f;
    float waitForHold = 1.5f;

    public void setStats(float dash, float dashcd, float speed)
    {
        levelUpDash = dash;
        levelUpDashCd = dashcd;
        levelUpSpeed = speed;
    }

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
        canDie = true;
    }

    public void cantDie()
    {
        canDie = false;
    }

    private void gameOver()
    {
        if (canDie)
        {
            // Game over
        }
    }

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

    public void lightsOn()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

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
            dash = 1.85f * (1.2f*levelUpDash);
            dashCd = 1.5f * actualLevelCd;
            waitForHold = 0.75f;
        }
        else dash = 1.1f;
    }

    private void PlayerDashHold_performed(InputAction.CallbackContext obj)
    {
        float actualLevelCd = (1f - (1f - levelUpDashCd));
        if (waitForHold > 0)
        {
            dash = 2.75f * levelUpDash;
            dashCd = 3f * (1.5f * actualLevelCd);
        }
        else dash = 1.15f;
    }

    private void Update()
    {

        moveDir = playerControls.ReadValue<Vector2>();
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

        if(dash > -0.1f)
        {
            dash -= (1 * Time.deltaTime);
        }
        
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
