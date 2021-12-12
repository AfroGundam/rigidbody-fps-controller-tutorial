
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public Camera tpsCam;

    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;
    public float impactForce = 20f;
    public float fireRate = 15f;
    // Update is called once per frame

    private float nextTimeToFire = 0f;
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time+ 1f/fireRate;
            Shoot();
        }
        
    }

    void Shoot()
    {
        muzzleFlash.Play(); 
        RaycastHit hit;
        if(Physics.Raycast(tpsCam.transform.position, tpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target!= null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impGO,2f);

        }
    }

}
