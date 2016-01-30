using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }

	public int Level { get; set; }

	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Level = 0;

		InvokeRepeating("ChangeLevel", 2, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeLevel()
	{
		print("Level change!");
		Level++;
		WaveManager.Instance.LevelChange();
	}
}
