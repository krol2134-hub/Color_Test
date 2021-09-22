using System;
using UnityEngine;


public class InputPlayer : MonoBehaviour
{
    public static event Action OnFirstClick;
    public static bool IsMove => Input.GetMouseButton(0);
    
    private bool wasFirstClick;

    public static float GetXDelta()
    {
        return Input.touchCount > 0 ? Input.GetTouch(0).deltaPosition.x / 5 : Input.GetAxis("Mouse X");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !wasFirstClick)
        {
            FirstClickProcess();
        }
    }

    private void FirstClickProcess()
    {
        wasFirstClick = true;
        OnFirstClick?.Invoke();
    }
}
