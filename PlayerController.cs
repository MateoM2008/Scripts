    using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;

    public float speed = 5f;
    public float jumpForce = 7f;

    private bool isGrouded;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        rd.linearVelocity = new Vector2(
            move * speed,
            rd.linearVelocity.y
        );

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrouded)
        {
            rd.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );

            isGrouded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrouded = true;
        }
    }
}
