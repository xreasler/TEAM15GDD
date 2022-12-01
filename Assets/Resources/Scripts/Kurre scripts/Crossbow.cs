using UnityEngine;
using UnityEngine.InputSystem;

public class Crossbow : MonoBehaviour
{
    public float range;
    [SerializeField] private float shootCD;
    private bool _readyToShoot = true;
    
    public enum TargetTag{ Player,Enemy }

    public TargetTag targetTag;
    
    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && _readyToShoot)
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F) && !_readyToShoot)
        {
            Debug.Log("reloading");
        }
    }

    private void Shoot()
    {
        _readyToShoot = false;
        RaycastHit hit;
        
        Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);
        
        
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Current Target set to  " + targetTag);

            IDamageable currenTarget = hit.collider.gameObject.GetComponent<IDamageable>();
            if (currenTarget != null)
            {
                currenTarget.Damage();
                Debug.Log("Attacking  "+ targetTag +"  Remaining HP:  " + currenTarget.Health);
            }
        }
        Invoke("Reload", shootCD);
    }
    private void Reload()
    {
        _readyToShoot = true;
    }
    
}
