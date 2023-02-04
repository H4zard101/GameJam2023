using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;


    public Transform EffectPoint;
    public GameObject pariclessSystem;

    public void TakeDamage(float damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Debug.Log("Enemy Dead");
            Death();
        }
    }

    public void Death()
    {

        Instantiate(pariclessSystem, EffectPoint.position, EffectPoint.rotation);
        Debug.Log("particles");
        Destroy(gameObject);
        
    }
}
