using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private int dir = 1;
    private Vector2 worldSize;
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
        worldSize = GetWorldSize();       
    }

    Vector2 GetWorldSize() {

        Camera cam = Camera.main;       
        Vector2 worldSize = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0));
        return worldSize;
        
    }

    void Update()
    {
        transform.Translate(transform.right * dir * speed * Time.deltaTime);


        

        if (transform.position.x > worldSize.x || transform.position.x < -worldSize.x) Destroy(this.gameObject);
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
