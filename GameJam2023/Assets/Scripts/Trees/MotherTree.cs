using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MotherTree : GameTrees
{
    public GameObject Mother_Tree;
    public GameObject[] sourceTreesUnderInfluence;
    public int maximumNuberOfSourceTrees = 3;
    public int motherTreeLevel = 1;
    public float Health = 400;
    public Slider TreeHealth;
    public Slider TreeHealth2;
    public Slider TreeHealth3;
    public Slider TreeHealth4;
    public Slider TreeHealth5;
    public TMP_Text TreeHealthText;

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
        TreeHealth.value = Mathf.Clamp(Health, 0, 100);
        TreeHealth2.value = Mathf.Clamp(Health-100, 0, 100);
        TreeHealth3.value = Mathf.Clamp(Health-200, 0, 100);
        TreeHealth4.value = Mathf.Clamp(Health-300, 0, 100);
        TreeHealth5.value = Mathf.Clamp(Health-400, 0, 100);
        TreeHealthText.text = Mathf.Clamp(Health, 0, 9999).ToString();
        
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
