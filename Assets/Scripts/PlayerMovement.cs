using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    [SerializeField] private float speed;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,_body.velocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            _body.velocity = new Vector2(_body.velocity.x,speed);
        }
    }
}
