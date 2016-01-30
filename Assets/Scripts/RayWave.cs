using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// A colored wave consisting o a ray, pointing towards the player and traveling forward and a particle system for effects
/// </summary>
public class RayWave : MonoBehaviour {

	public float secondsToDestination = 6f;
	public ParticleSystem pS;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	

	public void BeginMove()
	{
		transform.DOMove(transform.forward * secondsToDestination, secondsToDestination).OnComplete(Kill);
	}

	private void Kill()
	{
		gameObject.SetActive(false);
	}

	public ParticleSystem Ps
	{
		get { return pS; }
		set { pS = value; }
	}
}
