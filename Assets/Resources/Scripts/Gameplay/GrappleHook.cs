using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private CharacterController _cInput;
    private ThirdPersonController _cMovement;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugHitpointTransform;
    private State _state;
    private Vector3 grapplePosition;
    [SerializeField] private float reachedGrapplePosition;
    
    private enum State
    {
        Normal,
        GrappleflyingPlayer
    }

    private void Start()
    {
        _cMovement = GetComponent<ThirdPersonController>();
        _cInput = GetComponent<CharacterController>();
    }

    private void Update()
    {
        switch (_state)
        {
            default:
                case State.Normal:
                GrappleStart();
                break;
            case State.GrappleflyingPlayer:
                GrappleMovement();
                break;
        }
        
        
        GrappleStart();
    }

    private void GrappleStart()
    {
        Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out  RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugHitpointTransform.position = raycastHit.point;
                grapplePosition = raycastHit.point;
                _state = State.GrappleflyingPlayer;
            }  
        }
    }

    
    private void GrappleMovement()
    {
        Vector3 grappleDir = (grapplePosition - transform.position).normalized;

        float grappleSpeedMin = 10f;
        float grappleSpeedMax = 40f;
        float grappleSpeed = Mathf.Clamp(Vector3.Distance(transform.position, grapplePosition), grappleSpeedMin, grappleSpeedMax);
        float grapplSpeedMultiplier = 2f;
        
        _cInput.Move(grappleDir * grappleSpeed *  grapplSpeedMultiplier * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, grapplePosition) < reachedGrapplePosition)
        {
            _state = State.Normal;
            ResetGravity();
        }

    }

    private void ResetGravity()
    {
        _cMovement._verticalVelocity = 0f;
    }

}
