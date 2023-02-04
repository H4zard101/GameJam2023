using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{


    public Transform sourceTreeEffectPoint;
    public float damage = 20.0f;




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MotherTree")
        {
            //Destroy(gameObject);
            collision.gameObject.GetComponent<MotherTree>().TakeDamage(damage);
            Debug.Log(collision.gameObject.GetComponent<MotherTree>().Health.ToString());
            
        }
        if(collision.gameObject.tag == "Tree")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<TreeSource>().TakeDamage(damage);
            Debug.Log(collision.gameObject.GetComponent<TreeSource>().Health.ToString());
        }
    }
}
