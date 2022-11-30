using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random=UnityEngine.Random;

public class enemy : MonoBehaviour, IDamageable
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Tobase
    }

    [Space]
    [Header("   DUE NOTE!! ")]
    [Space]
    [Header("Melee weapon attached in skeleton root Hand_R")]
    [Space(25)]
    
    
    
    [SerializeField]
     private EnemyState enemyState;

    private Animator anim;

    private NavMeshAgent navAgent;

    private Rigidbody rb;

   // public MeleeWeapon m_myWeapon;
    
    
    [Header("Settings")]
    [SerializeField]
    [Tooltip("Player Object")]
    private Transform player;
    public Vector3 OriginalPosition;
    [Tooltip("Detection Range")]
    [Range(0.0f, 10.0f)]
    public float range = 1f;
    private float homeRange = 1f;
    
    
    
    
    //private IDamageable currenTarget;
    
    public float nextAttack = -1f;
    public float attackRate = 0.5f;
    public float Health { get; set; }
    
    
    
    
    
    [SerializeField] private Transform blood;
    
    
    [Header("AudioSources")]
    
    public AudioSource AggroAudio;
    public AudioSource AttackAudio;
    public AudioSource DieAudio;
    public RandomAudioPlayer HitAudio;
    public AudioSource FrontStepAudio;
    public AudioSource BackStepAudio;
    
    [Header("Audioclips")]
    public AudioClip Aggro;
    public AudioClip HitClip;

    private bool AggroUse;
    private bool HitUse;
    [SerializeField]
    private float _health;
    


    
    
    
    public static readonly int hashInPursuit = Animator.StringToHash("InPursuit");
    public static readonly int hashAttack = Animator.StringToHash("Attack");
    public static readonly int hashHit = Animator.StringToHash("Hit");
    public static readonly int hashGrounded = Animator.StringToHash("Grounded");
    public static readonly int hashSpotted = Animator.StringToHash("Spotted");
    public static readonly int hashNearBase = Animator.StringToHash("NearBase");
    public static readonly int hashDeath = Animator.StringToHash("Death");

    public static readonly int hashIdleState = Animator.StringToHash("Idle");




    private void Awake()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();


    }

    private void Start()
    {
        enemyState = EnemyState.Idle;
        anim.SetBool(hashGrounded, true);
        OriginalPosition = transform.position;
       // m_myWeapon.SetOwner(gameObject);
        AggroUse = false;
        Health = _health;
        player = GameObject.Find("PlayerArmature").transform;

    }

    void Update()
    {
        
        switch (enemyState)
        {

            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Tobase:
                break;

        }

        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            
            
            
            enemyState = EnemyState.Chase;
            anim.SetBool(hashNearBase, false);
            anim.SetBool(hashSpotted, true);
            if (AggroUse == false)
            {
                AggroAudio.PlayOneShot(Aggro); 
            }

            AggroUse = true;
            Invoke("Chase", 1);
            
        }

        if (Vector3.Distance(transform.position, player.position) > range)
        {
            enemyState = EnemyState.Tobase;
            anim.SetBool(hashInPursuit, false);
            navAgent.SetDestination(OriginalPosition);
            
            if (Vector3.Distance(transform.position, OriginalPosition) <= homeRange)
            {
                enemyState = EnemyState.Idle;
                anim.SetBool(hashInPursuit, false);
                anim.SetBool(hashSpotted, false);
                anim.SetBool(hashNearBase, true);
                AggroUse = false;
                navAgent.SetDestination(OriginalPosition);
                
                


            }

            
        }
        
        
        
    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            
            navAgent.isStopped = true;
            anim.SetBool(hashInPursuit, false);
            anim.SetBool(hashAttack, true);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        navAgent.isStopped = false;
        anim.SetBool(hashInPursuit, true);
        anim.SetBool(hashAttack, false);
    }


    public void Hitted()
     {
         anim.SetTrigger(hashHit);
     }

     public void Chase()
     {
         anim.SetBool(hashInPursuit, true);
         navAgent.SetDestination(player.position);
     }

     public void Death()
     {
         
         anim.SetBool(hashDeath, true);
         gameObject.GetComponent<enemy>().enabled = false;
         navAgent.enabled = false;
     }

     

     public void Damage()
     {
         Health--;
         Debug.Log("GHOUL: I TAKE DAMAGE");
         if (Health > 0)
         {
             Hitted();
         }
         
         
         
         if (Health == 0)
         {
             Death();
         }
     }

     
     

    







}