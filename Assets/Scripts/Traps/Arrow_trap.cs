using System;
using UnityEngine;

public class Arrow_trap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float _coolDownTimer;
    private void Attack()
    {
        _coolDownTimer = 0;
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        _coolDownTimer += Time.deltaTime;
        if (_coolDownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
