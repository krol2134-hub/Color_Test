using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private float loseRestartDelay = 2f;
    [SerializeField] private float winRestartDelay = 2f;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        Player.OnAllBallsLost += AllBallsLostHandler;
        Finish.OnFinishLevel += FinishLevelHandler;
    }

    private void OnDisable()
    {
        Player.OnAllBallsLost -= AllBallsLostHandler;
        Finish.OnFinishLevel -= FinishLevelHandler;
    }

    private void FinishLevelHandler()
    {
        StartCoroutine(RestartWithDelay(winRestartDelay));
    }

    private void AllBallsLostHandler()
    {
        StartCoroutine(RestartWithDelay(loseRestartDelay));
    }

    private IEnumerator RestartWithDelay(float restartDelay)
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
