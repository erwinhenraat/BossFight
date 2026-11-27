

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour
{
    [SerializeField] private int life = 10;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float speed = 5f;
    private int currentWaypointIndex = 0;
    private Animator animator;

    private AnimationState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private enum AnimationState { 
        Idle,
        Patrol,
        Attack,
        Die
    }
    void Start() {
        animator = GetComponent<Animator>();
        state = AnimationState.Idle;
    }
    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    private void Patrol() {
        if (waypoints[currentWaypointIndex].position.x < transform.position.x)
        {
            transform.Translate(-transform.right * Time.deltaTime * speed);
        }
        else if (waypoints[currentWaypointIndex].position.x > transform.position.x)
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }

        float distance = Mathf.Abs(waypoints[currentWaypointIndex].transform.position.x - transform.position.x);

        if (distance < (speed * Time.deltaTime / 2))
        {
            if (currentWaypointIndex < waypoints.Count-1)
            {
                currentWaypointIndex++;
            }
            else
            {
                currentWaypointIndex = 0;
            }
        }
    }    
    void ChangeState(AnimationState newState)
    {
        state = newState;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Patrol");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Die");
        animator.SetTrigger(newState.ToString());
        //Debug.Log($"State : {newState}");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }             
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
