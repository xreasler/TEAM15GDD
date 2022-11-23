using System;
using Unity.VisualScripting;
using StarterAssets;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
  private LineRenderer lr;
  private Vector3 grapplePoint;
  [SerializeField] private LayerMask grappleLayer = new LayerMask();
  [SerializeField] private Transform gunTip, player;
  [SerializeField] private float grappleLenght;
  private SpringJoint Joint;
  
  private ThirdPersonController _cMovement;
  [SerializeField] private Transform grappleIndicatorPos;
  [SerializeField] private GameObject grappleIndicator;

  [SerializeField] private GameObject rigidParent;
  [SerializeField] private GameObject playerParent;
  


  private void Awake()
  {
    lr = GetComponent<LineRenderer>();
    
    _cMovement = GetComponent<ThirdPersonController>();
  }

  private void Start()
  {
    playerParent.transform.parent = rigidParent.transform;
  }

  private void Update()
  {
    
    if (Input.GetMouseButtonDown(0))
    {
      StartGrapple();
    }
    else if (Input.GetMouseButtonUp(0))
    {
      StopGrapple();
    }
    
    CanGrapple();
    
  }

  private void LateUpdate()
  {
    DrawRope();
  }


  private void StartGrapple()
  {
    RaycastHit hit;
    
    Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
    Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);


    if (Physics.Raycast(ray, out hit, grappleLenght, grappleLayer))
    {
      grapplePoint = hit.point;
      Joint = player.AddComponent<SpringJoint>();
      Joint.autoConfigureConnectedAnchor = false;
      Joint.connectedAnchor = grapplePoint;

      float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

      Joint.maxDistance = distanceFromPoint * 0.8f;
      Joint.minDistance = distanceFromPoint * 0.25f;


      Joint.spring = 4.5f;
      Joint.damper = 7f;
      Joint.massScale = 4.5f;

      lr.positionCount = 2;
      
      _cMovement.enabled = false;
    }
  }

  private void DrawRope()
  {
    if(!Joint)
      return;
    
    lr.SetPosition(0, gunTip.position);
    lr.SetPosition(1, grapplePoint);
  }
  
  private void StopGrapple()
  {
    lr.positionCount = 0;
    Destroy(Joint);
    _cMovement.enabled = true;
  }


  private void CanGrapple()
  {
    Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
    Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);

    if (Physics.Raycast(ray, out RaycastHit raycastHit, grappleLenght, grappleLayer))
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
