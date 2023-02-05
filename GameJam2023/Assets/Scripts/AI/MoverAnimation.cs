using Pathfinding;
using System;
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

	public float DamageValue = 5f;

	private void Awake()
	{
		ai = GetComponent<IAstarAI>();
		tr = GetComponent<Transform>();
	}

    private void Start()
    {
		anim = transform.GetChild(0).GetComponent<Animator>();
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

        try
        {
			GameObject tree = GetComponent<AISetDestination>().target.gameObject;
			if (tree != null)
			{
				Debug.Log("tree" + tree);
				tree.GetComponent<GameTrees>().TakeDamage(DamageValue);
			}

			StartCoroutine(DestroyWithDelay());
		}
        catch(Exception ex)
        {

        }
		
	}

	private IEnumerator DestroyWithDelay()
    {
		yield return new WaitForSeconds(0.1f);
		AudioPlayback.PlayOneShot(AudioManager.Instance.references.enemyDeathEvent, null);//Enemy death oneshot
		 AudioPlayback.PlayOneShot(AudioManager.Instance.references.enemyExplodeEvent, this.gameObject); 
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
