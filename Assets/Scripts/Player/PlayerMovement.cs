using UnityEditor;
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

    [Header("Coyote Time")] 
    [SerializeField] private float coyoteTime;

    [Header("Multiple Jumps")]
    [SerializeField] private int numberOfJumps;

    [Header("Wall Jumping")] [SerializeField]
    private float wallJumpX;

    [SerializeField] private float wallJumpY;

    private int _jumpCounter;

    private float _coyoteCounter;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) && _body.velocity.y>0)
        {
            _body.velocity = new Vector2(-_body.velocity.x,_body.velocity.y/2);
        }

        if (OnWall())
        {
            _body.gravityScale = 0;
            _body.velocity = Vector2.zero;
        }
        else
        {
            _body.gravityScale = 7;
            _body.velocity = new Vector2(_horizontalInput*speed,_body.velocity.y);
            if (IsGrounded())
            {
                _coyoteCounter = coyoteTime;
                _jumpCounter = numberOfJumps;
            }
            else
            {
                _coyoteCounter -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        if (_coyoteCounter <= 0 && !OnWall() && _jumpCounter<=0) return; 
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything

        SoundManager.Instance.PlaySound(jumpSound);

        if (OnWall())
            WallJump();
        else
        {
            if (IsGrounded())
                _body.velocity = new Vector2(_body.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (_coyoteCounter > 0)
                    _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                else
                {
                    if (_jumpCounter > 0)
                    {
                        _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                        _jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            _coyoteCounter = 0;
        }

    }

    private void WallJump()
    {
        _body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*wallJumpX,wallJumpY));
        _wallJumpCooldown = 0;
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
