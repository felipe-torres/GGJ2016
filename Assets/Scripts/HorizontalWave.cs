using UnityEngine;
using System.Collections;

public class HorizontalWave : MonoBehaviour {

	private ParticleSystem pS;
	public float decreaseRate = 0.01f;

	// Use this for initialization
	void Start () {
		pS = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateShape();
	}

	void UpdateShape()
	{		
		var sh = pS.shape;
		sh.radius -= decreaseRate;		
		CheckCollisionWithPlayer();
	}

	void CheckCollisionWithPlayer()
	{
		var sh = pS.shape;
		if(sh.radius <= 0)
		{
			Player.Instance.WaveCollision();
			this.gameObject.SetActive(false);
		}
	}
}
