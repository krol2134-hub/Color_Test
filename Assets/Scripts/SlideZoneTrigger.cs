using System;
using UnityEngine;

public class SlideZoneTrigger : MonoBehaviour
{ 
    public static event Action OnSlideZoneEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            PlayerEnterInSlideZone();
        }
    }

    private void PlayerEnterInSlideZone()
    {
        OnSlideZoneEnter?.Invoke();
        Time.timeScale = 0.5f;
    }
}
