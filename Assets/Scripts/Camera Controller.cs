using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    private float _currentPositionX;
    private Vector3 _velocity=Vector3.zero;

    private void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPositionX,transform.position.y,transform.position.z),ref _velocity,speed);
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z-10);
    }

    public void MoveToNewRoom(Transform newRoomPosition)
    {
        _currentPositionX = newRoomPosition.position.x;
        
    }
}
