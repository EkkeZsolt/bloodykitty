using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKeyCode;

    public float speed = 5f;
    public float jumpForce = 15f;
    Rigidbody2D rb;
    bool isOnGround;
    float rayHeight = 0.6f;

    Vector2 direction;


    // Ha beállítasz egy spawnTransform-et az Inspectorban, akkor azt használjuk.
    // Ha nem, a Start() pillanatnyi pozícióját mentjük el.
    public Transform spawnTransform;

    //ráugrálás beállítás
    public float stompVerticalThreshold = 0.3f; // mennyivel magasabban kell lennie a találónak
    public float stompBounce = 6f; // mennyi visszapattanás kap a ráugró

    public Transform spawnPoint;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Respawn();
    }

    void Update()
    {
        MovePlayer();

        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayHeight);
        Debug.DrawRay(transform.position, Vector2.down * rayHeight, Color.red);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }

    void MovePlayer()
    {


        if (Input.GetKey(leftKey))
        {
            direction = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(rightKey))
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            direction = Vector2.zero;
        }


        if (Input.GetKeyDown(jumpKeyCode) && isOnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }

        Debug.Log($"Trigger hit: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Trap vagy Enemy Meghalok!");
            Die();
        }


        // csak másik Player-rel foglalkozunk
        if (!collision.gameObject.CompareTag("Player")) return;

        Player other = collision.gameObject.GetComponent<Player>();
        if (other == null) return;

        // Az elsõ kontaktus pontból megnézzük az ütközés normálját
        ContactPoint2D contact = collision.contacts[0];

        // Ha a normál felfelé mutat, akkor én felülrõl érkeztem
        bool iHitFromAbove = contact.normal.y > -0.5f;

        if (iHitFromAbove && rb.linearVelocity.y < 0f)
        {
            HandleStomp(other);
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }



    void HandleStomp(Player victim)
    {
        // 1) Optional: ha szeretnéd, hogy a ráugró "rákapaszkodjon" a célpontra, beállíthatod parentként
        // transform.SetParent(victim.transform);

        // 2) Kapjon egy kis bounce-ot vissza
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * stompBounce, ForceMode2D.Impulse);

        // 3) Meghal a victim -> respawn
        victim.Die();
    }

    // Meghalás: respawn a mentett spawnponthoz, és unparent-eljük a gyerekeket
    public void Die()
    {
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 1f);
    }

    void Respawn()
    {
        transform.position = spawnPoint.position;
        gameObject.SetActive(true);
    }

    


}


