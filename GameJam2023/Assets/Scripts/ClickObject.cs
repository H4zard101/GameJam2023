using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{

    private GameObject roots;
    public GameObject[] nearbyTrees;
    private GameObject nearestTree;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (nearbyTrees == null)
                nearbyTrees = GameObject.FindGameObjectsWithTag("Tree");


            if (Physics.Raycast(ray, out hit))
            {

                hit.transform.gameObject.GetComponent<RootTree>().active = true;


                //Find nearest tree in network to draw roots to
                /*foreach (GameObject tree in nearbyTrees)
                { 
                    if tree.transfom.position - this.gameObject.transform.position 
                    {
                        if nearestTree != null
                        {
                          if tree.transfom.position - this.gameObject.transform.position
                        }
                    }
                }
                */

                hit.transform.gameObject.GetComponent<TreeRoots>().start = this.gameObject;
                hit.transform.gameObject.GetComponent<TreeRoots>().end = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<TreeRoots>().Grow();


            }
        }
    }
}