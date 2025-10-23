using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector2 direction;
    float speed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Vector2.right; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformCollider"))
        {
            direction = -direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
