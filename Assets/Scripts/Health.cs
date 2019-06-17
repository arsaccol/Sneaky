using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float HitPoints = 100f;


    public void InflictDamage(float howMuchDamage)
    {
        HitPoints -= howMuchDamage;
        if(HitPoints <= 0f)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
