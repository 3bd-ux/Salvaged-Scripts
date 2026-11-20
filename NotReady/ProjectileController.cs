using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed = 5f;
    [SerializeField]private float _initialUpwardForce = 5;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _direction = (Player.Instance.transform.position + Vector3.up * 2 - transform.position).normalized;
    }
    void FixedUpdate()
    {
        _rb.linearVelocityX = _direction.x * _speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile hit: " + collision.gameObject.name);
            DeactivateProjectile();
        }
    }

    public void ActivateProjectile(Vector3 ProjectileOrigin)
    {
        transform.position = ProjectileOrigin;
        _rb.WakeUp();
        _rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(true);
        _rb.AddForce(Vector2.up * _initialUpwardForce, ForceMode2D.Impulse);
        _direction = (Player.Instance.transform.position + Vector3.up - transform.position).normalized;
    }

    public void DeactivateProjectile()
    {
        gameObject.SetActive(false);
        _rb.Sleep();
    }
}
