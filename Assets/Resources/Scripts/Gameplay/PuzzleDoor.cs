using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    private int intCount = 0; 

    public Transform _targetDoor;

    public Transform _targetCrystal;
    //
    // public Transform _targetLight;
    // public Transform _targetLight2;
    

    public void Update()
    {
        OpenMe();
        testitest();
    }

    public void AddCount()
    {
        intCount++;
    }

    public void OpenMe()
    {
        if (intCount == 5)
        {
             _targetCrystal.GetComponent<MiscSetup>().swapMat(); 
             _targetDoor.GetComponent<MiscSetup>().swapMat();
            
             _targetDoor.GetComponent<MiscSetup>().Open();
            //_targetLight.GetComponent<MiscSetup>().SwapLight();
            //_targetLight2.GetComponent<MiscSetup>().SwapLight();
             
        }
    }

    public void testitest()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddCount();
            Debug.Log("Adding Count   " + intCount);
        }
    }
    
}
