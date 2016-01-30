using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RayWave : MonoBehaviour {

	private ParticleSystem pS;
	public float secondsToDestination = 6f;

	// Use this for initialization
	void Start () {
		pS = GetComponent<ParticleSystem>();
	}

	void OnEnable()
	{
		//UpdatePosition();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void UpdatePosition()
	{		
		transform.DOMove(transform.forward, secondsToDestination);
	}

	void OnTriggerEnter(Collider other)
	{
		print("CollisionWithRayHasBegun");
		var sh = pS.shape;		
		Player.Instance.WaveCollision();
		
	}

	void OnTriggerStay(Collider other)
	{

	}

	void OnTriggerExit(Collider other)
	{
		print("CollisionWithRayHasEnded");
		this.gameObject.SetActive(false);
	}

	public void BeginMove()
	{
		transform.DOMove(transform.forward * secondsToDestination, secondsToDestination).OnComplete(Kill);
	}

	private void Kill()
	{
		gameObject.SetActive(false);
	}
}
