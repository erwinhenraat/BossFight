using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    float currentSpeed = 0;
    private Animator animator;
    private Rigidbody2D rigidbody;

    [SerializeField] private float maxJumpTime = 3f;

    [SerializeField] private float jumpForce = 50f;
    private float elapsedJumpTime = 0f;
    private bool isOnGround = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();      
        rigidbody = GetComponent<Rigidbody2D>();

        animator.Play("Idle",0,Random.Range(0,1));
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            animator.SetTrigger("Jump");
            animator.ResetTrigger("Idle");
            rigidbody.AddForceY(jumpForce);
            isOnGround = false;
        }
        


        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Walk");
            currentSpeed = moveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
            animator.SetTrigger("Idle");
            currentSpeed = 0;
        }        
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Walk");
            currentSpeed = -moveSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            animator.SetTrigger("Idle");
            currentSpeed = 0;
        }
        rigidbody.transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log(collision.collider.tag);
            isOnGround = true;
            Debug.Log("Landed");
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Jump");

        }

    }
    
}
