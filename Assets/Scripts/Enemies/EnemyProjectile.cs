using UnityEngine;

public class EnemyProjectile : Enemy_damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float _lifeTime;
    public void ActivateProjectile()
    {
        _lifeTime = 0;
        gameObject.SetActive(true);
        
    }

    private void Update()
    {
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
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
