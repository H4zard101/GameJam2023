using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{

    private GameObject roots;
    //roots VineSpreader = GetComponent<VineSpreader>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // do something with hit.transform.gameObject
                Debug.Log("Hit object: " + hit.transform.name);

                hit.transform.gameObject.GetComponent<RootTree>().active = true;
                hit.transform.gameObject.GetComponent<TreeRoots>().start = this.gameObject;
                hit.transform.gameObject.GetComponent<TreeRoots>().end = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<TreeRoots>().Grow();


            }
        }
    }
}