using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f;
    float jumpForce = 15f;
    Rigidbody2D rb;
    Vector2 direction;
    bool isOnGround;
    float rayHeight = 0.6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();

        // Földdetektálás
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, rayHeight);
        Debug.DrawRay(transform.position, Vector2.down * rayHeight, Color.red);

        if (direction.x != 0)
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
            
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isOnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // nullázza az Y sebességet
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}

 
