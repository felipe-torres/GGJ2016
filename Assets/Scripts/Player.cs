using UnityEngine;
using System.Collections;

/// <summary>
/// Player main controller
/// </summary>
public class Player : MonoBehaviour {

	public static Player Instance { get; set; } 

	public Camera camera;
	public Camera orthoCamera;

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
}
