using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private int dir = 1;
    public int Dir
    {
        get { return dir; }
        set { dir = value; }
    }
    public enum Origin { 
        player,
        boss
    }
    [SerializeField] private Origin origin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    public void SetOrigin(Origin o) {
        origin = o;
    }*/
    void Start() {
        GetComponent<SpriteRenderer>().flipX = dir > 0 ? false : true;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * dir * speed * Time.deltaTime);

        if (transform.position.x > 10 || transform.position.x < -10) Destroy(this.gameObject);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        if (collision.CompareTag("Boss") && origin == Projectile.Origin.player)
        {
            Debug.Log($"Boss is dead");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player") && origin == Projectile.Origin.boss)
        {
            Destroy(collision.gameObject);
            Debug.Log($"Player is dead");
        }
    }*/
   
}
