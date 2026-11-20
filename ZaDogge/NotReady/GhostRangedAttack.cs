using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostRangedAttack : MonoBehaviour
{

    [SerializeField] private float _shootInterval = 2f; // Time between shots
    [SerializeField] private int _poolSize = 5; // Number of projectiles in the pool
    [SerializeField] private ProjectileController _projectilePrefab; // Prefab for the projectile
    [SerializeField] private Transform _projectileParent; // Assign in inspector or create in Awake()
    private Queue<ProjectileController> _projectilePool = new Queue<ProjectileController>(); // Pool for projectiles
    private ProjectileController _projectile;
    private Coroutine _attackCoroutine;
    void Awake()
    {
        // Optionally create the parent if not assigned in inspector
        if (_projectileParent == null)
        {
            GameObject parentObj = new GameObject("Projectiles");
            _projectileParent = parentObj.transform;
        }

        for (int i = 0; i < _poolSize; i++)
        {
            _projectile = Instantiate(_projectilePrefab);
            
            _projectile.transform.SetParent(_projectileParent); // Parent the projectile
            _projectile.gameObject.SetActive(false); // Initially deactivate the projectile
            _projectilePool.Enqueue(_projectile); // Add to the pool
        }
    }
    void Start()
    {
        StartAttacking();
        
    }
    private IEnumerator AttackRoutine()
    {
        while (_projectilePool.Count > 0)
        {

            _projectile = _projectilePool.Dequeue();
            _projectile.gameObject.SetActive(false); // Ensure the projectile is inactive before activating

            _projectile.ActivateProjectile(transform.position);
            _projectilePool.Enqueue(_projectile); // Re-enqueue the projectile after use

            yield return new WaitForSeconds(_shootInterval);
        }
        Debug.LogWarning("No projectiles available in the pool!");


    }
    public void StartAttacking()
    {
        if (_attackCoroutine == null && _projectilePool.Count > 0)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    public void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
}