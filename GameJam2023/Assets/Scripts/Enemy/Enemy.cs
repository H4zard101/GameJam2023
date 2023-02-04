using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public float health = 100;

    public Transform EffectPoint;

    private Animator animationController;

    private void Start()
    {
        if(EffectPoint!=null)
            EffectPoint.GetComponent<ParticleSystem>().Stop();

        animationController = GetComponent<Animator>();
        animationController.SetTrigger("running");

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
