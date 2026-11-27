using UnityEngine;

public class Player : MonoBehaviour
{
    private enum AnimationState { 
        Idle,
        Walk,
        Jump,
        Shoot,
        Hit
    }
    private float currentSpeed = 0f;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private AnimationState state;
    private SpriteRenderer sr;
    private Vector2 playerInput;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 250f;
    [SerializeField] private GameObject projectile;    
    void Start()
    {
        animator = GetComponent<Animator>();      
        rigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ChangeState(AnimationState.Jump);        
    }       
    void Update()
    {
        playerInput = Vector2.zero;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        currentSpeed = moveSpeed * Time.deltaTime * playerInput.x;

        if (playerInput.x != 0) {
            Flip();            
        }
        if (Input.GetKeyDown(KeyCode.Space) && state!= AnimationState.Shoot) {
            ChangeState(AnimationState.Shoot);
            Shoot();
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeState(AnimationState.Jump);
                if (Jump()) { 
                
                }
            }
            Move();
        }
        else if (state == AnimationState.Jump)
        {
            if (playerInput.x != 0)
            {
                Move();
            }
        }
        else if (state == AnimationState.Shoot) {
           
            if (animator.GetAnimatorTransitionInfo(0).normalizedTime > 0.9f)
            {
                ChangeState(AnimationState.Idle);
            }
        }
        
    }
    bool Jump()
    {
        rigidbody.AddForceY(jumpForce);
        return true;
    }
    void Move()
    {
        transform.Translate(transform.right * currentSpeed);
    }
    void Shoot() {
        GameObject p = Instantiate(projectile);

        p.transform.position = transform.position + transform.up * 0.5f;
        p.GetComponent<Projectile>().Dir = sr.flipX ? -1 : 1;       
    }
    void Flip()
    {
        //Vector2 scale = new Vector2((playerInput.x >= 0) ? 1 : -1, 1);
        //transform.localScale = scale;
        sr.flipX = playerInput.x < 0 ? true:false;
    }
    void ChangeState(AnimationState newState)
    {
        state = newState;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Shoot");
        animator.SetTrigger(newState.ToString());
        //Debug.Log($"State : {newState}");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) {
            ChangeState(AnimationState.Idle); 
        }      
    }    
}
