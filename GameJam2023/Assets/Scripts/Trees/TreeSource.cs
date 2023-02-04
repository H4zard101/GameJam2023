using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSource : MonoBehaviour
{
    public GameObject SourceTree;
    public GameObject turret_type;
    public GameObject[] turretsUnderInfluence;
    private int treeSourceLevel = 0;
    public float Health = 100;
    public float HealthPerLevel = 50f;
    public float gridSize = 3f;
    private Vector3[] positions = new Vector3[4];

    public Transform EffectPoint;
    public GameObject pariclessSystem;

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

        Upgrade();
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
            Death();
        }
    }

    public void Upgrade()
    {
        if (treeSourceLevel < 4)
        {
            treeSourceLevel += 1;
            GameObject new_turret = Instantiate(turret_type, positions[treeSourceLevel - 1], Quaternion.identity);
            turretsUnderInfluence[treeSourceLevel - 1] = new_turret;
            Health += HealthPerLevel;
        }
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Instantiate(pariclessSystem, EffectPoint.position, EffectPoint.rotation);
    }

    void Death()
    {
        Destroy(this.gameObject);
        foreach (GameObject turret in turretsUnderInfluence)
        {
            Destroy(turret);
        }
    }
}
