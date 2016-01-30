using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }

	public int Level { get; set; }

	public ColorPalettes colorPalettes;
	private ColorPalette currentPalette;

	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Level = 0;
		ChooseRandomColorPalette();
		InvokeRepeating("ChangeLevel", 2, 10);
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

	private void ChooseRandomColorPalette()
	{
		CurrentPalette = colorPalettes.GetRandomPalette();
	}

	public ColorPalette CurrentPalette 
	{
		get { return currentPalette; }
		set { currentPalette = value; }
	}
}
