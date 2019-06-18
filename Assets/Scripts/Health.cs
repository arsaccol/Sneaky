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
        gib(100);
        gameObject.SetActive(false);
    }

    private void gib(int howManyGibs)
    {
        for(int i = 0; i < howManyGibs; ++i)
        {
            var newGib = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newGib.transform.position = transform.position;
            newGib.transform.localScale = new Vector3(.2f, .2f, .2f);

            newGib.GetComponent<Renderer>().material.color = Color.red;
            var gibPhysics = newGib.AddComponent<Rigidbody>();

            newGib.AddComponent<Health>();
            gibPhysics.AddForce(new Vector3(Random.Range(-100f, 100f), Random.Range(0f, 1f), Random.Range(-100f, 100f)));
        }


    }
}
