using UnityEngine;
using System.Collections;

public class ColorCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		print("CollisionWithRayHasBegun");
		GetComponent<SpriteRenderer>().color = other.GetComponent<RayWave>().Ps.startColor;
	}

	void OnTriggerStay(Collider other)
	{

	}

	void OnTriggerExit(Collider other)
	{
		print("CollisionWithRayHasEnded");
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}
