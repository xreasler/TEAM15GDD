using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private Rigidbody rb;
    

    private const float EPSILON = 0.1f;


    private Vector3 fDirection; 
    
    [SerializeField] private float speed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        fDirection = (player.position - transform.position).normalized;

        if ((transform.position - player.position).magnitude > EPSILON && gameObject.transform.parent != playerParent.transform)
        {
            rb.velocity = fDirection * speed;
        }
        
    }
}
