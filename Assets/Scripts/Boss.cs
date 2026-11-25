using UnityEngine;
using static Projectile;

public class Boss : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Projectile"))
        {
            Debug.Log($"Boss is dead");
            Destroy(gameObject);
        }
    }
}
