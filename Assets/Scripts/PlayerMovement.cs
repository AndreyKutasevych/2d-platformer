using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _animation;
    [SerializeField] private float speed;
    private bool _grounded;
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private static readonly int Run = Animator.StringToHash("Run");

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(horizontalInput*speed,_body.velocity.y);
        if (Input.GetKey(KeyCode.Space)&& _grounded)
        {
            Jump();
        }

        if (horizontalInput>0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput<-0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        _animation.SetBool("Run",horizontalInput!=0);
        _animation.SetBool("Grounded",_grounded);
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x,speed);
        _grounded = false;
        _animation.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Ground")
        {
            _grounded = true;
        }
    }
}
