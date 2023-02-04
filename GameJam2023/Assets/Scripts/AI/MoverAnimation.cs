using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAnimation : MonoBehaviour
{
	/// <summary>
	/// Animation component.
	/// Should hold animations "awake" and "forward"
	/// </summary>
	public Animator anim;

	/// <summary>
	/// Effect which will be instantiated when end of path is reached.
	/// See: <see cref="OnTargetReached"/>
	/// </summary>
	public GameObject endOfPathEffect;

	bool isAtDestination;

	IAstarAI ai;
	Transform tr;

	private void Awake()
	{
		ai = GetComponent<IAstarAI>();
		tr = GetComponent<Transform>();
	}

	/// <summary>Point for the last spawn of <see cref="endOfPathEffect"/></summary>
	protected Vector3 lastTarget;

	/// <summary>
	/// Called when the end of path has been reached.
	/// An effect (<see cref="endOfPathEffect)"/> is spawned when this function is called
	/// However, since paths are recalculated quite often, we only spawn the effect
	/// when the current position is some distance away from the previous spawn-point
	/// </summary>
	void OnTargetReached()
	{
		if (endOfPathEffect != null && Vector3.Distance(tr.position, lastTarget) > 1)
		{
			GameObject.Instantiate(endOfPathEffect, tr.position, tr.rotation);
			lastTarget = tr.position;
		}

        if (tr != null)
        {

			TreeManager.instance.RemoveTree(tr.gameObject);

			//if (tr.GetComponent<TreeSource>().Health > 0)
   //         {
   //             tr.GetComponent<TreeSource>().TakeDamage(5f);
   //         }
        }

		StartCoroutine(DestroyWithDelay());
	}

	private IEnumerator DestroyWithDelay()
    {
		yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}

	protected void Update()
	{
		if (ai.reachedEndOfPath)
		{
			if (!isAtDestination) OnTargetReached();
			isAtDestination = true;
		}
		else isAtDestination = false;

		// Calculate the velocity relative to this transform's orientation
		Vector3 relVelocity = tr.InverseTransformDirection(ai.velocity);
		relVelocity.y = 0;

		// Speed relative to the character size
		anim.SetFloat("NormalizedSpeed", relVelocity.magnitude / anim.transform.lossyScale.x);
	}
}
