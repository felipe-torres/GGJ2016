using UnityEngine;
using System.Collections;

/// <summary>
/// Player main controller
/// </summary>
public class Player : MonoBehaviour {

	public static Player Instance { get; set; } 
	public Sprite ColorColector;

	void Awake () 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WaveCollision() 
	{
		print("Wave has collided");
	}
}
