using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour, IDamageable
{
    public float Health { get; set; }
    
    public Transform Handler;
    public bool HaveCounted = false;
    public Material[] mainMat;

    Renderer rend;
    public static int i;


    public void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    public void Damage()
    {
        if (HaveCounted == false)
        {
            Debug.Log("BUTTON HIT CURRENT COUNT");
            HaveCounted = true;
            
            Handler.GetComponent<PuzzleDoor>().AddCount(); 
            if (i == 0)
            {
                rend.sharedMaterial = mainMat[0];
                i++;
            }
            else if (i == 1)
            {
                rend.sharedMaterial = mainMat[1];
                i--;
            }  
        }
        
        
    }
}
