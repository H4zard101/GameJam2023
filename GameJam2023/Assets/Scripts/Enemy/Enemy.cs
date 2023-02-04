using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public float health = 100;

    public Transform EffectPoint;
    public GameObject pariclessSystem;
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
            Death();
        }
    }

    public void Death()
    {
        Instantiate(pariclessSystem, EffectPoint.position, EffectPoint.rotation);   
        AudioPlayback.PlayOneShot(AudioManager.Instance.references.enemyDeathEvent, null);//Enemy death oneshot  
        Destroy(gameObject);
        
    }
}
