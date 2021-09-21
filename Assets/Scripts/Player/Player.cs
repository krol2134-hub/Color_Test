using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private int startBallAmount;
    [SerializeField] private float slideSpeed = 1f;
    [SerializeField] private float minSlideStep = 0.15f;
    [SerializeField] private float maxSlideStep = 1f;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballsParent;

    public static event Action OnAllBallsLost;
    public static event Action<int> OnBallsNumberChange;
    
    private bool isAllBallsLost;
    private bool isSlide;
    private float currentStep = 0.15f;
    private List<Ball> balls = new List<Ball>();

    private void OnEnable()
    {
        SlideZoneTrigger.OnSlideZoneEnter += OnSlideZoneEnterHandler;
    }

    private void OnSlideZoneEnterHandler()
    {
        ballsParent.rotation = Quaternion.identity;
        BallsRestructuring();
        isSlide = true;
    }

    private void Start()
    {
        SpawnNewBalls(startBallAmount);
    }
    
    private void SpawnNewBalls(int spawnNumber)
    {
        for (var i = 0; i < spawnNumber; i++)
        {
            var posToSpawn = balls.Count == 0 ? transform.position : GetPosByBallNormal(balls[0]);
            var newBall = Instantiate(ballPrefab, posToSpawn, Quaternion.identity, ballsParent);
            newBall.transform.localRotation = Quaternion.identity;
            balls.Add(newBall);
        }
    }

    private Vector3 GetPosByBallNormal(Ball ball)
    {
        var ballMesh = ball.BallMeshFitter.mesh;
        var randPos = ballMesh.normals[Random.Range(0, ballMesh.normals.Length)];
        var pos = ball.transform.position + randPos;
        return pos;
    }

    private void Update()
    {
        if (!isSlide || !InputPlayer.IsMove)
        {
            return;
        }

        var stepChangerNumber = InputPlayer.GetXDelta() * Time.deltaTime *  slideSpeed;
        ChangeStep(stepChangerNumber);
        BallsRestructuring();
    }

    private void ChangeStep(float newStepValue)
    {
        currentStep += newStepValue;
        currentStep = Mathf.Clamp(currentStep, minSlideStep, maxSlideStep);
    }
    
    private void BallsRestructuring()
    {
        var firstPos = balls.Count / 2 * currentStep;
        for (var i = 0; i < balls.Count; i++)
        {
            var currentBall = balls[i];
            Vector3 ballPos;
            if (i == 0)
            {
                ballPos = currentBall.transform.position;
                ballPos.x = -firstPos;
            }
            else
            {
                ballPos = balls[i - 1].transform.position;
                ballPos.x += currentStep;
            }
            
            currentBall.transform.position = ballPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAllBallsLost || isSlide)
        {
            return;
        }
        
        if (other.gameObject.TryGetComponent(out Gate gate) && gate.Active)
        {
            GateTriggerProcess(gate);
        }
    }

    private void GateTriggerProcess(Gate gate)
    {
        gate.PlayerCollision();
        var targetCount = GetTargetCount(gate.Types, gate.Number);
        if (targetCount > balls.Count)
        {
            SpawnNewBalls(targetCount - balls.Count);
        }
        else if (targetCount < balls.Count)
        {
            DestroyBalls(balls.Count - targetCount);
            if (targetCount <= 0)
            {
                LostBalls();
            }
        }

        OnBallsNumberChange?.Invoke(balls.Count);
    }

    private int GetTargetCount(GateType gateTypes, int number)
    {
        var numberToSpawn = gateTypes switch
        {
            GateType.Increase => balls.Count + number,
            GateType.Multiplier => balls.Count * number,
            GateType.Decrease => balls.Count - number,
            GateType.Divide => balls.Count / number
        };

        return numberToSpawn >= 0 ? numberToSpawn : 0;
    }

    private void DestroyBalls(int destroyNumber)
    {
        for (var i = 0; i < destroyNumber; i++)
        {
            var lastBox = balls.Last();
            balls.Remove(lastBox);
            Destroy(lastBox.gameObject);
        }
    }
    
    private void LostBalls()
    {
        isAllBallsLost = true;
        OnAllBallsLost?.Invoke();
    }
}
