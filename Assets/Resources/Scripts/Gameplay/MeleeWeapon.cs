using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
   public float nextAttack = -1f;
   public float attackRate = 0.5f;
   
   public enum TargetTag{ Player,Enemy }

   public TargetTag targetTag;


   private void Start()
   {
      Debug.Log("MY CURRENT TARGET IS  "  + targetTag );
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(targetTag.ToString()))
      {
         Debug.Log("Current Target set to  " + targetTag);
         IDamageable currenTarget = other.gameObject.GetComponent<IDamageable>();
         if (Time.time > nextAttack)
         {
            if (currenTarget != null)
            {
               currenTarget.Damage();
               nextAttack = Time.time + attackRate;
               Debug.Log("Attacking  "+ targetTag +"  Remaining HP:  " + currenTarget.Health);
            }
           
            

            
         }
         
            

      }
   }
    
}
