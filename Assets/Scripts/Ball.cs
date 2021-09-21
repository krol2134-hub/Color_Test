using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private Material[] ballMaterials;

    public MeshFilter BallMeshFitter { get; private set; }
    
    private void Start()
    {
        BallMeshFitter = GetComponent<MeshFilter>();
        PickRandomMaterial();
    }

    private void PickRandomMaterial()
    {
        var randMaterials = ballMaterials[Random.Range(0, ballMaterials.Length)];
        GetComponent<MeshRenderer>().material = randMaterials;
    }
}
