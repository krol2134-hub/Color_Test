using UnityEngine;

public class EndLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject winTextObj;
    [SerializeField] private GameObject loseTextObj;
    [SerializeField] private GameObject endLevelBgObj;

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
    
    private void AllBallsLostHandler()
    {
        endLevelBgObj.SetActive(true);
        loseTextObj.SetActive(true);
    }

    private void FinishLevelHandler()
    {
        endLevelBgObj.SetActive(true);
        winTextObj.SetActive(true);
    }
}
