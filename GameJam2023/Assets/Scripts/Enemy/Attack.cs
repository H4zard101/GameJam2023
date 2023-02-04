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
            Destroy(gameObject);
            AudioPlayback.PlayOneShot(AudioManager.Instance.references.treeHitEvent, null);//Damage tree oneshot
            collision.gameObject.GetComponent<MotherTree>().TakeDamage(damage);
            Debug.Log(collision.gameObject.GetComponent<MotherTree>().Health.ToString());
            
        }
        if(collision.gameObject.tag == "Tree")
        {
            Destroy(gameObject);
            AudioPlayback.PlayOneShot(AudioManager.Instance.references.treeHitEvent, null); //Oneshot damage tree
            collision.gameObject.GetComponent<TreeSource>().TakeDamage(damage);
            Debug.Log(collision.gameObject.GetComponent<TreeSource>().Health.ToString());
        }
    }
}
