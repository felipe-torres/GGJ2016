using UnityEngine;
using System.Collections;

/// <summary>
/// Trigger to detect rays and collect colored sand
/// </summary>
public class ColorCollector : MonoBehaviour {

	private float timeStartedCollecting = 0;
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
		timeStartedCollecting = Time.time;
	}

	void OnTriggerExit(Collider other)
	{
		print("CollisionWithRayHasEnded");
		GetComponent<SpriteRenderer>().color = Color.white;

		float amountCollected = Time.time - timeStartedCollecting;
		int colorIndex = ColorPalettes.GetColorIndexFromColor(other.GetComponent<RayWave>().Ps.startColor);

		//GameManager.Instance.sandJar.Fill(colorIndex, amountCollected/100);
		GameManager.Instance.sandJar.Fill(other.GetComponent<RayWave>().Ps.startColor);
	}
}
