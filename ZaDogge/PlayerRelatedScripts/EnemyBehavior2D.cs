using UnityEngine;

public class EnemyBehavior2D : MonoBehaviour
{

    public float moveSpeed = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private int direction = 1;
    BoxCollider2D playerCol;
    void Start()
    {
        playerCol = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        targetPosition = transform.position + Vector3.right * direction;

        if (spriteRenderer != null)
            spriteRenderer.flipX = direction < 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Debug.Log("Enemy collided with: " + collision.gameObject.name);
            direction *= -1;
        }
    }
}
