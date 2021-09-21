using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static event Action OnFinishLevel;
    private bool isFinishNow;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinishNow)
        {
            return;
        }

        var player = other.gameObject.GetComponent<Ball>();
        if (player)
        {
            isFinishNow = true;
            OnFinishLevel?.Invoke();
        }
    }
}
