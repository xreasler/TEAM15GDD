using System;
using StarterAssets;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private CharacterController _cInput;
    private ThirdPersonController _cMovement;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform grappleIndicatorPos;
    [SerializeField] private GameObject grappleIndicator;
    [SerializeField] private Transform grappleTransform;
    private float grappleshotSize;
    
    private State _state;
    private Vector3 _grapplePosition;
    [SerializeField] private float reachedGrapplePosition;
    [SerializeField] private float grappleLenght;
    [SerializeField] float grappleSpeedMultiplier = 2f;
    
    private enum State
    {
        Normal,
        GrappleThrown,
        GrappleflyingPlayer,
        
    }

    private void Awake()
    {
        grappleTransform.gameObject.SetActive(false);
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
                _cMovement.enabled = true;
                GrappleStart();
                    break;
            
                case State.GrappleThrown:
                    GrappleThrown();
                    break;
                
                case State.GrappleflyingPlayer:
                _cMovement.enabled = false;
                GrappleMovement();
                    break;
        }

        CanGrapple();
        GrappleStart();
    }

    private void GrappleStart()
    {
        Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out  RaycastHit raycastHit, grappleLenght, aimColliderLayerMask))
            {
                _grapplePosition = raycastHit.point;
                grappleshotSize = 0f;
                grappleTransform.gameObject.SetActive(true);
                grappleTransform.localScale = Vector3.zero;
                _state = State.GrappleThrown;
            }  
        }
    }


    private void GrappleThrown()
    {
        grappleTransform.LookAt(_grapplePosition);

        float grappleThrowSpeed = 40f;
        grappleshotSize += grappleThrowSpeed * Time.deltaTime;
        grappleTransform.localScale = new Vector3(0.5f, 0.5f, grappleshotSize);


        if (grappleshotSize >= Vector3.Distance(transform.position, _grapplePosition))
        {
            _state = State.GrappleflyingPlayer;
        }
    }
    
    private void GrappleMovement()
    {
        Vector3 grappleDir = (_grapplePosition - transform.position).normalized;

        float grappleSpeedMin = 10f;
        float grappleSpeedMax = 40f;
        float grappleSpeed = Mathf.Clamp(Vector3.Distance(transform.position, _grapplePosition), grappleSpeedMin, grappleSpeedMax);

        _cInput.Move(grappleDir * grappleSpeed *  grappleSpeedMultiplier * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, _grapplePosition) < reachedGrapplePosition)
        {
            grappleTransform.gameObject.SetActive(false);
            _state = State.Normal;
        }
    }


    private void CanGrapple()
    {
        Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, grappleLenght, aimColliderLayerMask))
        {
            grappleIndicator.SetActive(true);
            grappleIndicatorPos.position = raycastHit.point;
        }
        else
        {
            grappleIndicator.SetActive(false);
        }
    }

}
