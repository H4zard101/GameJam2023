using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float Speed = 20.0f;
    public float damage = 30.0f;

    public enum AmmoType
    {
        potato,
        carrot,
        raddish
    };

    public AmmoType ammoType;

    public void Start()
    {
        if(ammoType == AmmoType.potato)
        {
            damage = 30;
        }
        if (ammoType == AmmoType.carrot)
        {
            damage = 20;
        }
        if (ammoType == AmmoType.raddish)
        {
            damage = 40;
        }
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        Destroy(gameObject);
        target.GetComponent<Enemy>().TakeDamage(damage);
        AudioPlayback.PlayOneShot(AudioManager.Instance.references.enemyHitEvent, null);//One shot enemy hit
        return;
    }
}
