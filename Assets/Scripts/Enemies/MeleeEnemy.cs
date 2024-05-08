using System;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float colliderDistance;
    private Enemy_patrol _enemyPatrol;
    private Health _playerHealth;
    private float _cooldownTimer=Mathf.Infinity;
    private Animator _anim;

    private void Awake()
    {
        _enemyPatrol = GetComponentInParent<Enemy_patrol>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (PlayerInSight() && _cooldownTimer >= attackCooldown)
        {
            _cooldownTimer = 0;
            _anim.SetTrigger("melee_attack");
        }

        if (_enemyPatrol != null)
        {
            _enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right * (range * transform.localScale.x)*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z),
            0,Vector2.left,0,playerLayer);
        if (hit.collider != null)
        {
            _playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            _playerHealth.TakeDamage(damage);
        }
    }
}
