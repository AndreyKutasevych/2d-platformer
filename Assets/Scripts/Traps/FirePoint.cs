using System;
using System.Collections;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float damage;
    [SerializeField] private AudioClip fireSound;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private bool _active;
    private bool _triggered;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!_triggered)
            {
                StartCoroutine(FireActivation());
            }

            if (_active)
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator FireActivation()
    {
        _triggered = true;
        _sprite.color=Color.red;
        Physics2D.IgnoreLayerCollision(10,11,true);
        yield return new WaitForSeconds(activationDelay);
        _animator.SetBool("Activated",true);
        Physics2D.IgnoreLayerCollision(10,11,false);
        _active = true;
        SoundManager.Instance.PlaySound(fireSound);
        _sprite.color=Color.white;
        yield return new WaitForSeconds(activeTime);
        _active = false;
        _triggered = false;
        _animator.SetBool("Activated",false);
    }
}
