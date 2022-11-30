using UnityEngine;
using UnityEngine.InputSystem;

public class Crossbow : MonoBehaviour
{
    public float damage;
    public float range;
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        
        Vector3 crossHairPoint = new Vector3(Screen.width / 2f - 30f, Screen.height / 2f + 30f, 0f);
        Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);
        
        
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
