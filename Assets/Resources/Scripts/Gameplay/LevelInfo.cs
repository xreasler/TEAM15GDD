using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    
    

    [TextArea(3,10)]
    public string Input = "";

    public TMP_Text txtguide;
    
    public GameObject canvas;

    public bool Itriggered;

    void Start()
    {
        
        canvas.SetActive(false); 

    }


    void Update()
    {

        txtguide.text = Input;
        txtguide.ForceMeshUpdate(true);


    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Itriggered == false)
        {
            Itriggered = true;
            txtguide.ForceMeshUpdate(true);
            txtguide.text = txtguide.ToString();
            canvas.SetActive(true);  
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            canvas.SetActive(false);  
            
        }
    }
}
