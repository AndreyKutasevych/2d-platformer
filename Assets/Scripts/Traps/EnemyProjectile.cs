using System;
using UnityEngine;

public class EnemyProjectile : Enemy_damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float _lifeTime;
    private Animator _animator;
    private bool _hit;
    private BoxCollider2D _collider2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        _hit = false;
        _lifeTime = 0;
        gameObject.SetActive(true);
        _collider2D.enabled = true;
    }

    private void Update()
    {
        if(_hit)return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0,0);
        _lifeTime += Time.deltaTime;
        if (_lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _hit = true;
        base.OnTriggerEnter2D(other);
        _collider2D.enabled = false;
        if (_animator != null)
        {
            _animator.SetTrigger("Explode");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
