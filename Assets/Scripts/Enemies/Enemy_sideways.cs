using System;
using UnityEngine;

public class Enemy_sideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movingDistance;
    [SerializeField] private float speed;
    private bool _movingLeft;
    private float _rightEdge;
    private float _leftEdge;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Awake()
    {
        _leftEdge = transform.position.x - movingDistance;
        _rightEdge = transform.position.x + movingDistance;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x-speed*Time.deltaTime,transform.position.y,transform.position.z);
            }
            else
            {
                _movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x+speed*Time.deltaTime,transform.position.y,transform.position.z);

            }
            else
            {
                _movingLeft = true;
            }
        }
    }
}
