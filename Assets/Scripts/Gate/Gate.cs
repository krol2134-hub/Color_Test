using System;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateType types;
    [SerializeField] private int number;
    [SerializeField] private Material increaseMaterial;
    [SerializeField] private Material decreaseMaterial;
    [SerializeField] private Text gateText;

    public GateType Types => types;
    public int Number => number;
    public bool Active { get; private set; } = true;
    public event Action OnPlayerCollision;
    private void Start()
    {
        SwitchMaterial();
        SetTextByType();
    }

    private void SwitchMaterial()
    {
        var targetMat = types == GateType.Increase || types == GateType.Multiplier ? increaseMaterial : decreaseMaterial;
        GetComponent<MeshRenderer>().material = targetMat;
    }

    private void SetTextByType()
    {
        gateText.text = types switch
        {
            GateType.Increase => "+" + number,
            GateType.Multiplier => "x" + number,
            GateType.Decrease => "-" + number,
            GateType.Divide => "÷" + number,
        };
    }
    
    public void DeActive()
    {
        Active = false;
    }
    
    public void PlayerCollision()
    {
        Active = false;
        OnPlayerCollision?.Invoke();
    }


}
