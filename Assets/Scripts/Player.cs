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


    public Transform spawnTransform;

    //ráugrálás beállítás
    public float stompVerticalThreshold = 0.3f; // mennyivel magasabban kell lennie a ráugrónak
    public float stompBounce = 6f; // mennyi visszapattanást kap a ráugró

    public Transform spawnPoint;

    bool isOnIce = false;
    public float iceSlideFactor = 0.98f; // minél közelebb 1-hez, annál csúszósabb



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
        // alap mozgás
        float targetSpeed = direction.x * speed;

        if (isOnIce)
        {
            // ha jégen vagyunk, ne álljunk meg hirtelen — inkább interpoláljunk a jelenlegi sebesség felé
            float smoothedX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, 0.02f);
            rb.linearVelocity = new Vector2(smoothedX, rb.linearVelocity.y);
        }
        else
        {
            // normál mozgás
            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
        }
    

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
            if (!isOnIce)
            {
                direction = Vector2.zero;
            }
            
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

        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = true;
        }


        // csak olyannal foglalkozunk aminek a tagja Player
        if (!collision.gameObject.CompareTag("Player")) return;

        Player other = collision.gameObject.GetComponent<Player>();
        if (other == null) return;

        // Az elsõ kontaktus pontból megnézzük az ütközés normálját
        ContactPoint2D contact = collision.contacts[0];

        // Ha a normálvektor felfele mutat akkor én vagyok felül
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
            if (collision.gameObject.activeInHierarchy)
            {
                transform.SetParent(null);
            }
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isOnIce = false;
        }
    }



    void HandleStomp(Player victim)
    {
        
        // transform.SetParent(victim.transform);

        // egy kicsit visszapattan
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * stompBounce, ForceMode2D.Impulse);

        // Meghal akire ráugrottunk
        victim.Die();
    }

    // Meghalás kezelése, inaktívvá tesszük majd 1 mp múlva meghíjuk rá a Respawn functiont
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


