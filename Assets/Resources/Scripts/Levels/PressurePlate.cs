using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Animator _animator;

    public Transform _targetDoor;

    public Transform _targetCrystal;
    
    public Transform _targetLight;
    public Transform _targetLight2;
    public Transform _targetLight3;
    public Transform _targetLight4;

    

    public static readonly int hashActivated = Animator.StringToHash("Triggered");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger(hashActivated);
        _targetCrystal.GetComponent<MiscSetup>().swapMat();
        _targetDoor.GetComponent<MiscSetup>().swapMat();
        _targetDoor.GetComponent<MiscSetup>().Open();
        _targetLight.GetComponent<MiscSetup>().SwapLight();
        _targetLight2.GetComponent<MiscSetup>().SwapLight();
        _targetLight3.GetComponent<MiscSetup>().SwapLight();
        _targetLight4.GetComponent<MiscSetup>().SwapLight();
     



    }
}
