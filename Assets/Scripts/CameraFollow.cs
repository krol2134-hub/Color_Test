using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private Vector3 offset;

    private bool canMove = true;
    
    private void Start()
    {
        SlideZoneTrigger.OnSlideZoneEnter += () => canMove = false;
    }

    private void LateUpdate()
    {
        if (canMove)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        var position = transform.position;
        position.z = (targetToFollow.position + offset).z;
        transform.position = position;
    }
}