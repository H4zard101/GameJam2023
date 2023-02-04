using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float Speed = 20.0f;

    public enum AmmoType
    {
        The_Irish_Favorite,
        Eggcorn,
        PineCone
    };

    public AmmoType ammoType;
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
        //Destroy(target.gameObject);
        return;
    }
}
