using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    public float slideFactor = 0.95f; // minél közelebb van 1-hez, annál jobban csúszik

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // fokozatosan lassítjuk, nem állítjuk meg hirtelen
                rb.linearVelocity = new Vector2(rb.linearVelocity.x * slideFactor, rb.linearVelocity.y);
            }
        }
    }
}

