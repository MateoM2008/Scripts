using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ajuste de Movimiento")]
    public float speed = 3f;
    private bool movinRigth = true;

    [Header("Detectores")]
    public Transform grounCheck;
    public float distanciaToGround = 1.5f;
    public float distanciaToWall = 1.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(
            movinRigth ? speed : -speed,
            rb.velocity.y
        );

        Vector2 direccion = movinRigth ? Vector2.right : Vector2.left;

        RaycastHit2D isGroundAhead = Physics2D.Raycast(
            grounCheck.position,
            Vector2.down,
            distanciaToGround,
            groundLayer
        );

        RaycastHit2D isWallsAhead = Physics2D.Raycast(
            transform.position,
            direccion,
            distanciaToWall,
            groundLayer
        );

        if (isGroundAhead.collider == null || isWallsAhead.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movinRigth = !movinRigth;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        if (grounCheck != null)
        {
            Gizmos.DrawRay(
                grounCheck.position,
                Vector2.down * distanciaToGround
            );
        }

        Vector2 direccion = movinRigth ? Vector2.right : Vector2.left;

        Gizmos.DrawRay(
            transform.position,
            direccion * distanciaToWall
        );
    }
}