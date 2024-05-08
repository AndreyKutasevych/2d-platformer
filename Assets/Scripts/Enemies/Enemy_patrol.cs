using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_patrol : MonoBehaviour
{
    [Header("PatrolPoints")]
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftEdge;
    [Header("Enemy")] [SerializeField] 
    private Transform enemy;
    [Header("Movement parameters")]
    [SerializeField] private float speed;

    [SerializeField] private float idleTime;
    private float _idleTimer;

    private bool _movingLeft;

    private Vector3 initScale;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving",false);
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        _idleTimer += Time.deltaTime;
        anim.SetBool("moving",false);
        if (idleTime <= _idleTimer)
        {
            _movingLeft = !_movingLeft;
        }
    }
    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        anim.SetBool("moving",true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x)*direction,initScale.y,initScale.z);
        enemy.position = new Vector3(enemy.position.x+Time.deltaTime*direction*speed,enemy.position.y,enemy.position.z);
    }
}
