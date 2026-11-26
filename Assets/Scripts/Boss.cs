

using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int life = 10;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private int currentWaypointIndex = 0;
    private float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //todo
        // waypoints werken met index nummer ipv waypont in variabele kopieren


        currentWaypoint = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.position.x < transform.position.x)
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }
        else if (currentWaypoint.position.x > transform.position.x) {
            transform.Translate(-transform.right * Time.deltaTime * speed);
        }

        if (transform.position.x - currentWaypoint.transform.position.x < 0.01f) { 
            currentWaypoint = waypoints
        }
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
