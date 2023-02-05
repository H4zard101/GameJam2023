using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSource : GameTrees
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
            TreeManager.instance.RemoveTree(gameObject);
            Death();
        }
    }

    public void SetTurret(string type)
    {
        Debug.LogWarning("SetTurret Called :" + type);

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

        Upgrade(type);
    }

    public void Upgrade(string turretType = "StandardTurret")
    {

        if (turret_type != null)
        {
            if (treeSourceLevel < 4)
            {
                treeSourceLevel += 1;


                GameObject turretToInstantiate = turret_type;

                //set turret type based on the UI Selection 
                switch (turretType)
                {
                    case "StandardTurret":
                        Debug.LogWarning("SetTurret Called : StandardTurret");

                        turretToInstantiate.GetComponent<TurretLockOn>().turretType = TurretLockOn.TurretType.StandardTurret;
                        break;
                    case "SniperTurret":
                        Debug.LogWarning("SetTurret Called : SniperTurret");

                        turretToInstantiate.GetComponent<TurretLockOn>().turretType = TurretLockOn.TurretType.SniperTurret;
                        break;
                    case "RapidFireTurret":
                        Debug.LogWarning("SetTurret Called : RapidFireTurret");

                        turretToInstantiate.GetComponent<TurretLockOn>().turretType = TurretLockOn.TurretType.RapidFireTurret;
                        break;
                    default:
                        Debug.LogWarning("SetTurret Called : d");

                        turretToInstantiate.GetComponent<TurretLockOn>().turretType = TurretLockOn.TurretType.StandardTurret;
                        break;
                }


                GameObject new_turret = Instantiate(turretToInstantiate, positions[treeSourceLevel - 1], Quaternion.identity);
                if (new_turret == null)
                {
                    Debug.LogWarning("turret is null");

                }
                turretsUnderInfluence[treeSourceLevel - 1] = new_turret;
                Health += HealthPerLevel;
            }
        }
    }
    public override void TakeDamage(float damage)
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
