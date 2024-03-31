using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            if (other.transform.position.x<transform.position.x)
            {
                cameraController.MoveToNewRoom(nextRoom);
            }
            else
            {
                cameraController.MoveToNewRoom(previousRoom);
            }
        }
    }
}
