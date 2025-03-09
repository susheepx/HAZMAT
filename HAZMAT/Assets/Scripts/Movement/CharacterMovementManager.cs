using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovementManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed =3, walkBackSpeed =2;
    public float runSpeed =7, runBackSpeed =5;
    public float crouchSpeed =2, crouchBackSpeed =1;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;
    #endregion
    
    CharacterController controller;
    
    //ground check
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    //gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    //movement states
    BaseMovementState currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState(); 
    public CrouchState Crouch = new CrouchState();

    [HideInInspector] public Animator anim;

    //key press checks
    public Button mediaPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        checkKeys();
        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(BaseMovementState state) {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove() {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool isGrounded() {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity() {
        if(!isGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }

    private void checkKeys() {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mediaPlayer.onClick.Invoke(); 
        }
    }
}
