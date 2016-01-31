using UnityEngine;
using System.Collections;

/// <summary>
/// Contains game's main flow, manages level changes and detects winning or losing condition
/// </summary>
public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }

	public int Level { get; set; }

	public ColorPalettes colorPalettes;
	private ColorPalette currentPalette;

	public Level CurrentLevel;

	public SandJar sandJar;

	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Level = 0;
		ChooseRandomColorPalette();
		InvokeRepeating("ChangeLevel", 2, 30);
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
		sandJar.Initialize(currentPalette);
		SandJar.CreateTargetSandJar(currentPalette);
	}

	public ColorPalette CurrentPalette 
	{
		get { return currentPalette; }
		set { currentPalette = value; }
	}
}
