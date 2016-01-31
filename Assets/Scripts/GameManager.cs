using UnityEngine;
using System.Collections;
using DG.Tweening;

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

	public bool isGameOver = false;

	void Awake() 
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Restart();
		
	}

	private void Restart () 
	{
		isGameOver = false;
		Level = 0;
		ChooseRandomColorPalette();
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
		StartCoroutine(ShowTargetJar());
	}

	public void StartGameOver(bool hasWon)
	{
		isGameOver = true;
		CancelInvoke("ChangeLevel");
		StartCoroutine(GameOverSequence(hasWon));

		
	}

	private IEnumerator GameOverSequence(bool hasWon)
	{
		// Destroy current jars
		foreach (Transform child in sandJar.targetSandJarParent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		foreach (Transform child in sandJar.playerSandJarParent.transform) {
			GameObject.Destroy(child.gameObject);
		}

		AudioManager.Instance.ResetAll();

		yield return new WaitForSeconds(2);		

		// Restart all
		Restart();
	}

	private IEnumerator ShowTargetJar()
	{
		Player.Instance.orthoCamera.DOOrthoSize(0.2f, 2f);
		yield return new WaitForSeconds(1f);
		sandJar.targetSandJarParent.transform.DORotate(new Vector3(15, 0, 15), 0f);

		yield return new WaitForSeconds(0.5f);
		// Appear at the player's screen
		sandJar.targetSandJarParent.transform.parent.parent = Player.Instance.orthoCamera.transform;
		sandJar.targetSandJarParent.transform.parent.DOLocalMove(new Vector3(0, -0.07f, 0.54f), 1f);
		yield return new WaitForSeconds(1f);
		sandJar.targetSandJarParent.transform.parent.DORotate(new Vector3(0, 360, 0), 3, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

		// Wait and then position at the screen corner
		yield return new WaitForSeconds(2f);
		
		Player.Instance.orthoCamera.DOOrthoSize(0.7f, 2f);
		Vector3 screenInWorldSpace = Player.Instance.orthoCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
		sandJar.targetSandJarParent.transform.parent.DOLocalMove(new Vector3(screenInWorldSpace.x*3, screenInWorldSpace.y/4, 0.54f), 2f);

		yield return new WaitForSeconds(2f);
		StartCoroutine(ShowEmptyJar());

		
	}

	private IEnumerator ShowEmptyJar()
	{
		Player.Instance.orthoCamera.DOOrthoSize(0.2f, 2f);
		sandJar.playerSandJarParent.transform.DORotate(new Vector3(15, 0, 15), 0f);

		yield return new WaitForSeconds(0.5f);
		// Appear at the player's screen
		sandJar.playerSandJarParent.transform.parent.parent = Player.Instance.orthoCamera.transform;
		sandJar.playerSandJarParent.transform.parent.DOLocalMove(new Vector3(0, -0.07f, 0.54f), 2f);
		yield return new WaitForSeconds(2f);
		sandJar.playerSandJarParent.transform.parent.DORotate(new Vector3(0, 360, 0), 3, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

		// Wait and then position at the screen corner
		yield return new WaitForSeconds(2f);
		
		Player.Instance.orthoCamera.DOOrthoSize(0.7f, 2f);
		Vector3 screenInWorldSpace = Player.Instance.orthoCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0f));
		sandJar.playerSandJarParent.transform.parent.DOLocalMove(new Vector3(screenInWorldSpace.x*3, screenInWorldSpace.y/4, 0.54f), 2f);

		InvokeRepeating("ChangeLevel", 2, 15);
	}

	public ColorPalette CurrentPalette 
	{
		get { return currentPalette; }
		set { currentPalette = value; }
	}
}
