using UnityEngine;

public class LineGate : MonoBehaviour
{
    [SerializeField] private Gate leftGate;
    [SerializeField] private Gate rightGate;

    private void Start()
    {
        leftGate.OnPlayerCollision += () =>
        {
            rightGate.DeActive();
        };
        
        rightGate.OnPlayerCollision += () =>
        {
            leftGate.DeActive();
        };
    }
    
}
