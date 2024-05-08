using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator _anim;
    private bool _dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer _spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool _invulnerable;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _anim = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (_invulnerable) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            _anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
            SoundManager.Instance.PlaySound(damageSound);
        }
        else
        {
            if (!_dead)
            {
                _anim.SetTrigger("Dead");
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                _dead = true;
                SoundManager.Instance.PlaySound(deathSound);
            }
        }
    }
    public void ReplenishHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        _invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            _spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            _spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        _invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
