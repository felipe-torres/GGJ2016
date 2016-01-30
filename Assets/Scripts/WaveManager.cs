using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

/// <summary>In charge of wave creation and wave destruction, based on a pooling pattern.</summary>
public class WaveManager : MonoBehaviour {

    public static WaveManager Instance { get; set; }

    // wave pool
	private List<GameObject> waves;
	public GameObject WavePrefab;
    public int maxWaves = 10;
    public GameObject WavePoolParent;

    private bool hasStartedSpawn = false;

    public float spawnRadius = 20;
    public float spawnHeight = 2;

    void Awake()
    {
    	Instance = this;
    }

	// Use this for initialization
	void Start () 
    {
		InitializeWavePool();
	}

    /// <summary>Instantiates all waves.</summary>
	public void InitializeWavePool()
    {
        waves = new List<GameObject>();

        for(int i=0; i<maxWaves; i++)
        {
            GameObject wave = (GameObject)Instantiate(WavePrefab);
            wave.transform.parent = WavePoolParent.transform;
            wave.SetActive(false);
            waves.Add(wave);
        }
    }

    /// <summary>"Instantiates" a wave.</summary>
    public GameObject GetWave()
    {
    	for(int i=0; i<waves.Count; i++)
        {
            if(!waves[i].activeInHierarchy)
            {
            	waves[i].SetActive(true);
            	return waves[i];
            }
        }

        return null;
    }

    /// <summary>Triggered on level change in GameManager.ChangeLevel()</summary>
    public void LevelChange()
    {
        // Manage Spawn of waves
        hasStartedSpawn = false;
        StopCoroutine("SpawnRepeating");
        StartCoroutine("SpawnRepeating", GameManager.Instance.Level);
    }

    /// <summary>Spawns waves according to level with a period and an itemless period.</summary>
    IEnumerator SpawnRepeating(int level)
    {
        // Check current level...
        // Dont spawn anything in level 0
        // lv 1 - Spawn 1 - num waves for each player that doesnt have waves
        // lv 2- 
        // Check if it is spawn time. 

        // Timer? wait for timeToSpawn
        // Perhaps... the more time you in the game, the more frequent the spawn
        // Every time the level changes, it must invoke the SpawnRepeating function, alongside the level

        hasStartedSpawn = true;
        float wavePeriod;

        switch (level)
        {
            case 1:
                wavePeriod = 8;
                break;
            case 2:
                wavePeriod = 7;
                break;
            case 3:
                wavePeriod = 6;
                break;
            case 4:
                wavePeriod = 5;
                break;
            default:
                wavePeriod = 5;
                break;
        }

        while (hasStartedSpawn)
        {            
            //print("start itemless period");
            //print("start item period");
            StartCoroutine(Spawnwaves(wavePeriod));
            yield return new WaitForSeconds(wavePeriod);
        }

        yield return null;

    }

    /// <summary>Spawns a number of waves along a period of time.</summary>
    IEnumerator Spawnwaves(float wavespawnPeriod)
    {
        int num2Spawn = 1; // Wave number function goes here

        for (int i = 0; i < num2Spawn; i++)
        {
            Vector3 circfundRand = (new Vector3(Random.Range(-1f,1f), spawnHeight, Random.Range(-1f,1f)).normalized * spawnRadius);


            GameObject Wave = GetWave();
            if(Wave)
            {
            	// Choose random color from color palette
            	Color[] currentPaletteColors = GameManager.Instance.CurrentPalette.Colors;
            	Color waveColor = currentPaletteColors[Random.Range(0, currentPaletteColors.Length)];
            	Wave.GetComponent<RayWave>().Ps.startColor = waveColor;

                Wave.transform.localPosition = circfundRand;
                Wave.transform.LookAt(Vector3.zero);
                Wave.GetComponent<RayWave>().BeginMove();               
            }
            
            yield return new WaitForSeconds(Random.Range(0.0f, wavespawnPeriod)); // Spawns at random times inside the item spawn period       
        }
    }
}
