using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Ammunition;
    public float RateOfFire;
    public float Range = 50f;

    public float WeaponDamage = 40f;

    enum FiringType
    {
        BoltAction,
        SemiAuto,
        FullAuto,
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") > 0f)
            Shoot();
        
    }

    void Shoot()
    {
        Debug.DrawRay(transform.position, transform.forward * Range, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * Range, out hit, Range))
        {
            Health health;
            if(health = hit.transform.GetComponent<Health>())
            {
                health.InflictDamage(WeaponDamage);
            }

        }
    }
}
