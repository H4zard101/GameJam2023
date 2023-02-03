using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLockOn : MonoBehaviour
{

    public void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
        }

    }
}
