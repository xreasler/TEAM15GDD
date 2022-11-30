using Unity.VisualScripting;
using StarterAssets;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
  private LineRenderer _lr;
  private Vector3 _grapplePoint;
  [SerializeField] private LayerMask grappleLayer;
  [SerializeField] private Transform gunTip, player;
  [SerializeField] private float grappleLenght;
  private SpringJoint _joint;

  private CharacterController _cInput;
  private ThirdPersonController _cMovement;
  [SerializeField] private Transform grappleIndicatorPos;
  [SerializeField] private GameObject grappleIndicator;
  
  [SerializeField] private Rigidbody rb;
  [SerializeField] private GameObject rbParent;


  [SerializeField] private float spring;
  [SerializeField] private float damper;
  [SerializeField] private float massScale;

  public bool swinging;
  

  private void Awake()
  {
    _lr = GetComponent<LineRenderer>();
    
    Physics.IgnoreLayerCollision(6, 8);
    
    _cInput = GetComponent<CharacterController>();
    _cMovement = GetComponent<ThirdPersonController>();
    swinging = false;
  }




  private void Update()
  {

    if (Input.GetKeyDown(KeyCode.G))
    {
      rbParent.transform.parent = null;
      StartGrapple();
    }
    else if (Input.GetKeyUp(KeyCode.G))
    {
      StopGrapple();
      rb.transform.position = gunTip.transform.position;
      rbParent.transform.parent = gameObject.transform;
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
      rbParent.SetActive(true);
      _grapplePoint = hit.point;
      _joint = player.AddComponent<SpringJoint>();
      _joint.autoConfigureConnectedAnchor = false;
      _joint.connectedAnchor = _grapplePoint;

      float distanceFromPoint = Vector3.Distance(player.position, _grapplePoint);

      _joint.maxDistance = distanceFromPoint * 0.2f;
      _joint.minDistance = distanceFromPoint * 0.1f;


      _joint.spring = spring;
      _joint.damper = damper;
      _joint.massScale = massScale;

      _lr.positionCount = 2;
      
      rb.useGravity = true;
      swinging = true;
      _cMovement.Gravity = 0;
      _cInput.enabled = false;
      
      gameObject.transform.parent = rbParent.transform;
    }
  }

  private void DrawRope()
  {
    if(!_joint)
      return;
    
    _lr.SetPosition(0, gunTip.position);
    _lr.SetPosition(1, _grapplePoint);
  }
  
  private void StopGrapple()
  {
    rbParent.SetActive(false);
    gameObject.transform.parent = null;
    
    _lr.positionCount = 0;
    Destroy(_joint);
    rb.useGravity = false;
    swinging = false;
    _cMovement.Gravity = -15f;
    rb.velocity = new Vector3(0, 0, 0);
    _cInput.enabled = true;
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
