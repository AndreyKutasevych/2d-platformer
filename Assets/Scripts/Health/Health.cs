using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    private Animator _animator;
    private bool _isDead;
    public float CurrentHealth { get; private set; }
    [SerializeField] [Header("Iframes")] private float invulnerabilityDuration;
    [SerializeField] private float flashesAmount;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage,0,startingHealth);
        if (CurrentHealth > 0)
        {
            _animator.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());

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

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10,11,true);
        for (int i = 0; i < flashesAmount; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0,0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration/flashesAmount/2);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration/flashesAmount/2);
        }
        Physics2D.IgnoreLayerCollision(10,11,false);
    }
}
