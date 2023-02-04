using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSource : MonoBehaviour
{
    public GameObject SourceTree;
    public GameObject[] turretsUnderInfluence;
    public int maximumNumberOfTurrets = 3;
    public int treeSourceLevel = 1;
    public float Health = 100;

    public void Awake()
    {
        SourceTree = this.gameObject;
        turretsUnderInfluence = new GameObject[maximumNumberOfTurrets];
    }

    private void Update()
    {
        if(Health <= 0)
        {
            Destroy(this.gameObject);
            foreach(GameObject turret in turretsUnderInfluence)
            {
                Destroy(turret);
            }
        }
        
    }
}
