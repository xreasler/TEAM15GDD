using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour, IDamageable
{
    public float Health { get; set; }
    
    public Transform Handler;
    public bool HaveCounted = false;
    
    public void Damage()
    {
        if (HaveCounted == false)
        {
            HaveCounted = true;
            Handler.GetComponent<PuzzleDoor>().AddCount(); 
        }
        
        
    }
}