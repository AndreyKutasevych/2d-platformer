using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform _currentCheckpoint;
    private Health _playerHealth;
    private UImanager _uiManager;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _uiManager = FindObjectOfType<UImanager>();
    }

    public void CheckRespawn()
    {
        if (_currentCheckpoint == null)
        {
            _uiManager.GameOver();
        }
        else
        {
            transform.position = _currentCheckpoint.position;
            _playerHealth.Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint"&&other!=null)
        {
            _currentCheckpoint = other.transform;
            SoundManager.Instance.PlaySound(checkpointSound);
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
