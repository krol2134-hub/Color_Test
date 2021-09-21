using System;
using UnityEngine;


public class InputPlayer : MonoBehaviour
{
    public static event Action OnFirstClick;
    public static bool IsMove => Input.GetMouseButton(0);
    
    private bool wasFirstClick;
    private float lastXPos;

    public static float GetXDelta()
    {
#if UNITY_EDITOR
        return Input.GetAxis("Mouse X");
#else
        return Input.GetTouch(0).deltaPosition.x;
#endif
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !wasFirstClick)
        {
            lastXPos = Input.mousePosition.x;

            if (!wasFirstClick)
                FirstClickProcess();
        }
    }

    private void FirstClickProcess()
    {
        wasFirstClick = true;
        OnFirstClick?.Invoke();
    }
}
