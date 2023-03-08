using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLockOn : MonoBehaviour
{
    public Transform rotationPart;
    private Transform target;
    public float range = 10.0F;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject carrotBullet;
    public GameObject raddishBullet;
    public GameObject potatoBullet;

    public GameObject standard;
    public GameObject sniper;
    public GameObject rapid;


    public enum TurretType
    {
        StandardTurret,
        SniperTurret,
        RapidFireTurret

    };
    public TurretType turretType;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if(turretType == TurretType.StandardTurret)
        {
            range = 20.0f;
            fireRate = 1.0f;
            bulletPrefab = potatoBullet;
            standard.SetActive(true);
            rapid.SetActive(false);
            sniper.SetActive(false);

        }
        if (turretType == TurretType.RapidFireTurret)
        {
            range = 10.0f;
            fireRate = 3.0f;
            bulletPrefab = carrotBullet;
            standard.SetActive(false);
            rapid.SetActive(true);
            sniper.SetActive(false);
        }
        if (turretType == TurretType.SniperTurret)
        {
            range = 40.0f;
            fireRate = 0.75f;
            bulletPrefab = raddishBullet;
            standard.SetActive(false);
            rapid.SetActive(false);
            sniper.SetActive(true);
        }
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ai");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = lookRotation.eulerAngles;

        // For some reason the X has to be - overwise the barrel poits up side down 
        rotationPart.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);


        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        AudioPlayback.PlayOneShot(AudioManager.Instance.references.turretShootEvent, null);//One Shot turret fire
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
