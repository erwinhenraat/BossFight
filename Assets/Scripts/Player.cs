using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    private enum AnimationState { 
        Idle,
        Walk,
        Jump,
        Shoot,
        Hit
    }
    float currentSpeed = 0;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private AnimationState state;
    private Vector2 playerInput;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 50f;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();      
        rigidbody = GetComponent<Rigidbody2D>();
        ChangeState(AnimationState.Jump);

    }   
    // Update is called once per frame
    void Update()
    {
        playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        currentSpeed = moveSpeed * Time.deltaTime * playerInput.x;


        if (playerInput.x != 0) {
            FlipScale();
            
        }
        if (state == AnimationState.Idle || state == AnimationState.Walk)
        {
            
            if (playerInput.x != 0)
            {                    
                ChangeState(AnimationState.Walk);                         
            }
            else
            {
                ChangeState(AnimationState.Idle);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeState(AnimationState.Jump);
                Jump();
            }
            Move();
        } else if (state == AnimationState.Jump) {
            if (playerInput.x != 0)
            {
                Move();
            }
        }
    }
    void Jump()
    {
        rigidbody.AddForceY(250);
    }
    void Move()
    {
        transform.Translate(transform.right * currentSpeed);
    }
    void FlipScale()
    {
        Vector2 scale = new Vector2((playerInput.x >= 0) ? 1 : -1, 1);
        transform.localScale = scale;
    }
    void ChangeState(AnimationState newState)
    {
        state = newState;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Jump"); 
        animator.SetTrigger(newState.ToString());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) {
            ChangeState(AnimationState.Idle);
            Debug.Log("go idle");
        }      
    }    
}
