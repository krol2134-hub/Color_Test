using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        var position = transform.position;
        position.z = (targetToFollow.position + offset).z;
        transform.position = position;
    }
}