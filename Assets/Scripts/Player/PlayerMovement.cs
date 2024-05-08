using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _animation;
    private BoxCollider2D _boxCollider;
    private float _wallJumpCooldown;
    private float _horizontalInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    { 
        _horizontalInput = Input.GetAxis("Horizontal");

        if (_horizontalInput>0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(_horizontalInput<-0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        _animation.SetBool("Run",_horizontalInput!=0);
        _animation.SetBool("Grounded",IsGrounded());
        if (_wallJumpCooldown > 0.2f)
        {
            _body.velocity = new Vector2(_horizontalInput*speed,_body.velocity.y);
            if (OnWall() && !IsGrounded())
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
            {
                _body.gravityScale = 7f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                {
                    SoundManager.Instance.PlaySound(jumpSound);
                }
            }
        }
        else
        {
            _wallJumpCooldown+=Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x,jumpPower);
            _animation.SetTrigger("Jump");
        }
        else if(OnWall()&& !IsGrounded())
        {
            if (_horizontalInput==0)
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
            }
            _wallJumpCooldown = 0;
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return _horizontalInput == 0 && IsGrounded() && !OnWall();
    }
} 
