using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AISetDestination : MonoBehaviour
{
	public Transform target;
	IAstarAI ai;

    

    private float timeSinceLastCheck = 0f;
    public float checkInternal = 3f;

    private void Start()
    {
        FindClosestTree();
    }
    
    private void FindClosestTree()
    {
        // Keep track of the closest GameObject
        float closestDistance = float.MaxValue;

        // Loop through all GameObjects in the scene
        foreach (GameObject gameObject in TreeManager.instance.allTrees)
        {
            if (gameObject != null)
            {
                // Calculate the distance from the target transform to the current GameObject
                float distance = Vector3.Distance(transform.position, gameObject.transform.position);

                // Check if the current GameObject is closer than the previous closest GameObject
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = gameObject.transform;
                }
            }
            else
            {
                continue;
            }           
        }

        // Log the closest GameObject in the scene
        Debug.Log("Closest GameObject: " + target.name);
    }

    void OnEnable()
	{
		ai = GetComponent<IAstarAI>();
		// Update the destination right before searching for a path as well.
		// This is enough in theory, but this script will also update the destination every
		// frame as the destination is used for debugging and may be used for other things by other
		// scripts as well. So it makes sense that it is up to date every frame.
		if (ai != null) ai.onSearchPath += Update;
	}

	void OnDisable()
	{
		if (ai != null) ai.onSearchPath -= Update;
	}

	/// <summary>Updates the AI's destination every frame</summary>
	void Update()
	{
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= checkInternal)
        {
            timeSinceLastCheck = 0f;
            FindClosestTree();
        }

        if (target != null && ai != null) ai.destination = target.position;
	}
}
