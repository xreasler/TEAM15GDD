using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public float health;
    
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    private Material full;


    private void Start()
    {
        health = GameObject.FindWithTag("Player").GetComponent<IDamageable>().Health;
        full.renderQueue = 5000;
        
    }
    


    private void Update()
    { 
        health = GameObject.FindWithTag("Player").GetComponent<IDamageable>().Health;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
