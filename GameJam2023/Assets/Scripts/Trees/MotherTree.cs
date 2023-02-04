using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : MonoBehaviour
{
    public GameObject Mother_Tree;
    public GameObject[] sourceTreesUnderInfluence;
    public int maximumNuberOfSourceTrees = 3;
    public int motherTreeLevel = 1;
    public float Health = 400;

    public Transform EffectPoint;
    public GameObject pariclessSystem;
    public void Awake()
    {
        Mother_Tree = this.gameObject;
        sourceTreesUnderInfluence = new GameObject[maximumNuberOfSourceTrees];
    
    }
    private void Update()
    {
        if (Health <= 0)
        {
            TreeManager.instance.RemoveTree(gameObject);

            Destroy(this.gameObject);
            foreach (GameObject trees in sourceTreesUnderInfluence)
            {
                Destroy(trees);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Instantiate(pariclessSystem, EffectPoint.position, EffectPoint.rotation);
    }
}
