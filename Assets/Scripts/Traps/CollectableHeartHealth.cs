using System;
using UnityEngine;

public class CollectableHeartHealth : MonoBehaviour
{
    [SerializeField] private float hitPoint;
    [SerializeField] private AudioClip lifeGainSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            other.GetComponent<Health>().ReplenishHealth(hitPoint);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound(lifeGainSound);
        }
    }
}
