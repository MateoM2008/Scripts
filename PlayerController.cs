using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;
    public float jumpForce = 7f;

    private bool isGrouded;
    private bool facingRight = true;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // Animación de caminar
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("speed", speedAnimation);

        // Movimiento
        rd.linearVelocity = new Vector2(
            move * speed,
            rd.linearVelocity.y
        );

        // Guardar dirección
        if (move > 0)
        {
            facingRight = true;
        }
        else if (move < 0)
        {
            facingRight = false;
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrouded)
        {
            rd.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );

            isGrouded = false;
            animator.SetBool("isJump", true);
        }
    }

    // Se ejecuta después del Animator
  void LateUpdate()
{
    // Durante el salto, la animación está al lado contrario
    if (animator.GetBool("isJump"))
    {
        if (facingRight)
        {
            // Salta hacia la derecha
            spriteRenderer.flipX = false;
        }
        else
        {
            // Salta hacia la izquierda
            spriteRenderer.flipX = true;
        }
    }
    else
    {
        // Movimiento normal: NO MODIFICAR
        if (facingRight)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrouded = true;

        // Terminar inmediatamente la animación de salto
        animator.SetBool("isJump", false);

        // Actualizar inmediatamente la animación según el movimiento
        float move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(move));
    }
}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrouded = false;
        }
    }
}