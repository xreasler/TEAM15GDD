using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscSetup : MonoBehaviour
{
    public Material[] mainMat;

    Renderer rend;

    public static int index;

    public static int i;


    public bool ColorTriggered;
    public bool DoorTriggered;

    public Transform doorObject;
    public Vector3 OriginalPosition;

    public Transform targetPos;

    public Light lt;
    
    
    
    
    
    

    

    public void Start()
    {
        rend = GetComponent<Renderer> ();
        lt = GetComponent<Light>();
        rend.enabled = true;
        Debug.Log(mainMat);

        OriginalPosition = transform.position;
        
//  rend.sharedMaterial = mainMat [0];
    }

    

    public void swapMat()
    {
        if (ColorTriggered == false)
        {
            ColorTriggered = true;
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

    public void SwapLight()
    {
      lt.color = Color.blue;  
    }
    

    public void Open()
    {
        if (DoorTriggered == false)
        {
            DoorTriggered = true;
            StartCoroutine(DoorSlide()); 
       
        } 

        
        
    }
     IEnumerator DoorSlide()
     {

         float slideTime = 5f; 

         float t = 0;

         FindObjectOfType<AudioManager>().Play("DoorOpens");



        while (t < slideTime)

         {

             doorObject.position = Vector3.Lerp(OriginalPosition, targetPos.position, t / slideTime);

             t += Time.deltaTime;

             yield return null;

         }

     }


}
