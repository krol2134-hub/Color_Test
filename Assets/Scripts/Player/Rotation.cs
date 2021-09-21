using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float delayToChangeDirection = 1f;
    [SerializeField] private Transform rotateTarget = null;

    private Vector3 currentDirection;
    private bool canRotate = true;
    
    private void Start()
    {
        StartCoroutine(ChangeAxisWithDelay());
    }

    private IEnumerator ChangeAxisWithDelay()
    {
        while (true)
        {
            currentDirection = new Vector3(Random.value, Random.value, Random.value);
            yield return new WaitForSeconds(delayToChangeDirection);           
        }
    }
    
    private void OnEnable()
    {
        SlideZoneTrigger.OnSlideZoneEnter += OnSlideZoneEnterHandler;
    }

    private void OnDisable()
    {
        SlideZoneTrigger.OnSlideZoneEnter -= OnSlideZoneEnterHandler;
    }

    private void OnSlideZoneEnterHandler()
    {
        ResetRotate();
    }

    private void ResetRotate()
    {
        canRotate = false;
        rotateTarget.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (!canRotate)
        {
            return;;
        }
        
        rotateTarget.RotateAround(rotateTarget.position, currentDirection, speed * Time.deltaTime);
    }
}
