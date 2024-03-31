using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Animator _animator;
    private bool _isDead;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage,0,startingHealth);
        if (CurrentHealth > 0)
        {
            _animator.SetTrigger("Hurt");
            
        }
        else
        {
            if(!_isDead)
            {
                GetComponent<PlayerMovement>().enabled = false;
                _isDead = true;
                _animator.SetTrigger("Dead");
            }
            
        }
    }

    public void ReplenishHealth(float hitPoint)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + hitPoint,0,startingHealth);
    }

}
