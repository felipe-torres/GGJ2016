﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player Instance { get; set; } 

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
