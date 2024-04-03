using UnityEngine;

public class SpikeHead : Enemy_damage
{
    [Header("SpikeHead Attributes")]
    private Vector3 _destination;
    private bool _attacking;
    private Vector3[] _directions = new Vector3[4];
    [SerializeField] private LayerMask playerLayer; 
    [SerializeField] private float checkDelay;
    private float _checkTimer;
    [SerializeField] private float speed;
    [SerializeField] private float range;

    private void Update()
    {
        if (_attacking)
        {
            transform.Translate(_destination*Time.deltaTime*speed);
        }
        else
        {
            _checkTimer += Time.deltaTime;
            if (_checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        foreach (var t in _directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, t,range,playerLayer);
            if (hit.collider != null && !_attacking)
            {
                _attacking = true;
                _destination = t;
                _checkTimer = 0;
            }
        }
    }

    private void OnEnable()
    {
        Stop();
    }

    private void CalculateDirections()
    {
        _directions[0] = transform.right*range;
        _directions[1] = -transform.right*range;
        _directions[2] = transform.up*range;
        _directions[3] = -transform.up*range;
    }

    private void Stop()
    {
        _destination = transform.position;
        _attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag != "Player")
        {
            Stop();
        }
    }
}
