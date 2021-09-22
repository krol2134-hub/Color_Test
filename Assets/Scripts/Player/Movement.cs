using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float horizontalMoveSpeed = 1f;
    [SerializeField] private float xBoundOffset = 0.25f;
    [SerializeField] private GameObject floor;

    private float xMin, xMax;
    private bool canMove;
    private bool canHorizontalMove;

    
    private void Start()
    {
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        var floorHalfXScale = floor.transform.localScale.x / 2;
        xMin = 0 - floorHalfXScale + xBoundOffset;
        xMax = 0 + floorHalfXScale - xBoundOffset;
    }

    private void OnEnable()
    {
        InputPlayer.OnFirstClick += () =>
        {
            canHorizontalMove = true;
            canMove = true;
        };
        Player.OnAllBallsLost += () => canMove = false;
        SlideZoneTrigger.OnSlideZoneEnter += () => canHorizontalMove = false;
    }


    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        VerticalMove();
        HorizontalMove();
    }

    private void VerticalMove()
    {
        var currentPos = transform.position;
        currentPos.z += speed * Time.deltaTime;
        transform.position = currentPos;
    }

    private void HorizontalMove()
    {
        if (!canHorizontalMove || !InputPlayer.IsMove)
        {
            return;
        }

        var horSpeed = horizontalMoveSpeed * Time.deltaTime;
        var newXPos = InputPlayer.GetXDelta() * horSpeed;
        var currentPos = transform.position;
        currentPos.x += newXPos;
        currentPos.x = Mathf.Clamp(currentPos.x, xMin, xMax);
        transform.position = currentPos;
    }
}
