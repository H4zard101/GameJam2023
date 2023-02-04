using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;


    public Transform EffectPoint;

    private void Start()
    {
        EffectPoint.GetComponent<ParticleSystem>().Stop();
    }
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
        Destroy(gameObject);
        EffectPoint.GetComponent<ParticleSystem>().Play();
    }
}
