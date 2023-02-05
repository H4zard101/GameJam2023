using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public float health = 100;
    public Inventory20 inventory;

    public Transform EffectPoint;
    public GameObject pariclessSystem;
    private Animator animationController;

    private void Start()
    {
        if(EffectPoint!=null)
            EffectPoint.GetComponent<ParticleSystem>().Stop();

        animationController = GetComponent<Animator>();
        animationController.SetTrigger("running");
        inventory = FindObjectOfType<Inventory20>();
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
       
         
        Destroy(gameObject);
        inventory.GetComponent<Inventory20>().SeedAmount++;
        Debug.Log(inventory.SeedAmount.ToString());
        
    }
}
