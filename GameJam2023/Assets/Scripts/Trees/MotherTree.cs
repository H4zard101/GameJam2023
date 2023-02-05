using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotherTree : GameTrees
{
    public GameObject Mother_Tree;
    public GameObject[] sourceTreesUnderInfluence;
    public int maximumNuberOfSourceTrees = 3;
    public int motherTreeLevel = 1;
    public float Health = 400;

    private bool should_die = false;
    private float t = 0;

    public Transform EffectPoint;
    public GameObject pariclessSystem;
    public void Awake()
    {
        Mother_Tree = this.gameObject;
        sourceTreesUnderInfluence = new GameObject[maximumNuberOfSourceTrees];
    
    }
    private void Update()
    {
        if (should_die == true)
        {
            Debug.Log(t);
            t += Time.deltaTime;

            if (t > 2)
            {
                TreeManager.instance.RemoveTree(gameObject);

                Destroy(this.gameObject);
                foreach (GameObject trees in sourceTreesUnderInfluence)
                {
                    Destroy(trees);
                }

                SceneManager.LoadScene(2);
            }

        } else if (Health <= 0)
        {
            should_die = true;

            AudioManager.Instance.parameters.SetParamByLabelName(AudioManager.Instance.musicInstance, "PlayerDead", "PlayerDead");
        }
    }


    public override void TakeDamage(float damage)
    {
        Health -= damage;
        Instantiate(pariclessSystem, EffectPoint.position, EffectPoint.rotation);
    }
}
