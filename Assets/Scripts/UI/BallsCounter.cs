using UnityEngine;
using UnityEngine.UI;

public class BallsCounter : MonoBehaviour
{
    [SerializeField] private Image counterImage;
    [SerializeField] private Text counterText;

    private void Start()
    {
        counterImage.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputPlayer.OnFirstClick += OnFirstClickHandler;
        Player.OnBallsNumberChange += OnBallsNumberChangeHandler;
        Player.OnAllBallsLost += TurnOffDisplay;
        SlideZoneTrigger.OnSlideZoneEnter += TurnOffDisplay;
    }

    private void OnDisable()
    {
        InputPlayer.OnFirstClick -= OnFirstClickHandler;
        Player.OnBallsNumberChange -= OnBallsNumberChangeHandler;
        Player.OnAllBallsLost -= TurnOffDisplay;
        SlideZoneTrigger.OnSlideZoneEnter -= TurnOffDisplay;
    }

    private void OnFirstClickHandler()
    {
        counterImage.gameObject.SetActive(true);
        SetCounterText(1);
    }

    private void SetCounterText(int number)
    {
        counterText.text = number.ToString(); 
    }

    private void TurnOffDisplay()
    {
        counterImage.gameObject.SetActive(false);
    }

    private void OnBallsNumberChangeHandler(int ballsNumber)
    {
        SetCounterText(ballsNumber);
    }
}
