using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] _initialPosition;

    private void Awake()
    {
        _initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                _initialPosition[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateTheRoom(bool status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(status);
                enemies[i].transform.position = _initialPosition[i];
            }
        }
    }
}
