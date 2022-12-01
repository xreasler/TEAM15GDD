using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{



    

    public TMP_Text txtguide;
    public GameObject canvas;
    public GameObject parentHelper;
    public bool Itriggered;
    public int indexNumber;

    

    void Start()
    {
        
        parentHelper = GameObject.Find("HELPER");
        canvas.SetActive(false);
       

    }


    void Update()
    {

       
       




    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Itriggered == false)
        {

            txtguide.SetText(parentHelper.GetComponent<Phrases>().textInput[indexNumber], false);
            Debug.Log(txtguide);
            Itriggered = true;
            txtguide.ForceMeshUpdate(true);
            canvas.SetActive(true);  
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            txtguide.ForceMeshUpdate(true);
            canvas.SetActive(false);  
            
            
        }
    }

    
    
    
    
    
}
