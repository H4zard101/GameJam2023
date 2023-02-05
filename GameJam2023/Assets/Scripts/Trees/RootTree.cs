using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTree : MonoBehaviour
{
    public GameObject SourceTree;
    public GameObject turret_type;
    public GameObject[] turretsUnderInfluence;
    public bool active = false;
    private int treeSourceLevel = 0;
    public float Health = 100;
    public float HealthPerLevel = 50f;
    public float gridSize = 3f;
    private Vector3[] positions = new Vector3[4];
    private int treeCount = 0;
    private float timeBetweenCalls = 2f;
    private float timeSinceLastCall = 0f;


    private void Start()
    {
        positions[0] = transform.position;
        positions[0].x += gridSize;
        positions[0].y = 0;
        positions[1] = transform.position;
        positions[1].x -= gridSize;
        positions[1].y = 0;
        positions[2] = transform.position;
        positions[2].z += gridSize;
        positions[2].y = 0;
        positions[3] = transform.position;
        positions[3].z -= gridSize;
        positions[3].y = 0;


    }

    public void Awake()
    {
        SourceTree = this.gameObject;
        turretsUnderInfluence = new GameObject[4];
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            foreach(GameObject turret in turretsUnderInfluence)
            {
                Destroy(turret);
            }
        }

        
        timeSinceLastCall += Time.deltaTime;

        if (timeSinceLastCall >= timeBetweenCalls)
        {
            timeSinceLastCall = 0f;

            if (treeCount < 4)
            {
                treeCount++;
                Upgrade();
            }
        
    }

    }

    public void Upgrade()
    {
        if (treeSourceLevel < 4)
        {
            if (active == true)
            { 
            treeSourceLevel += 1;
            GameObject new_turret = Instantiate(turret_type, positions[treeSourceLevel - 1], Quaternion.identity);
            turretsUnderInfluence[treeSourceLevel - 1] = new_turret;
            Health += HealthPerLevel;
            }
        }
    }
}
