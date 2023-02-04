using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance = null;

    public List<GameObject> allTrees = new List<GameObject>();
    public GameObject motherTree;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);         
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        FindTrees();
    }

    private void FindTrees()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        allTrees = new List<GameObject>(trees);

        motherTree = GameObject.FindGameObjectWithTag("MotherTree");
    }

    public void RemoveTree(GameObject tree)
    {
        allTrees.Remove(tree);
    }



}
