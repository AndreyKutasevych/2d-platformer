using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform _currentCheckpoint;
    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = _currentCheckpoint.position;
        _playerHealth.Respawn();
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
