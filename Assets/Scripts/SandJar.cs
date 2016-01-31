using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Will contain data on the sand jar completeness (color and completeness percentage)
/// </summary>
public struct Jar 
{
	public List<Color> ColorsToFill;
	public List<float> PercentageFilled;
	public Color[] ColorsFilled;
	public int currentIndex;
};

public class SandJar : MonoBehaviour {	

	private Jar jar;
	public static int MaximumLayerNumber = 5;
	public GameObject sandLayerPrefab;
	
	public GameObject targetSandJarParent;
	public GameObject playerSandJarParent;

	// Use this for initialization
	void Start () {
	
	}

	public void Initialize(ColorPalette palette)
	{
		jar = SandJar.CreateTargetSandJar(palette);
		CreateTargetSandJarGameObject(jar, targetSandJarParent.transform);
	}

	public void Fill(int ColorIndex, float value)
	{
		print("Jar filled at: "+ColorIndex+" , with value of: "+ value);
		jar.PercentageFilled[ColorIndex] += value;

		CheckColorFilled(ColorIndex);
	}

	public void Fill(Color color)
	{
		//print("Jar filled at: "+ColorIndex+" , with value of: "+ value);
		jar.ColorsFilled[jar.currentIndex] = color;
		AddLayerToPlayerJar(color, jar.currentIndex);
		jar.currentIndex++;

		if(jar.currentIndex >= jar.ColorsToFill.Count)
		{
			// Check results in game manager
			print("Jar is full");
			GameManager.Instance.StartGameOver(CompareJars());
		}
	}

	private bool CompareJars()
	{
		bool result = true;

		for (int i = 0; i < jar.ColorsToFill.Count; i++) 
		{
			print(jar.ColorsToFill[i].r + " " + jar.ColorsFilled[i].r);

			result &= Mathf.Approximately(jar.ColorsToFill[i].r, jar.ColorsFilled[i].r);

			if(!result) return false;
		}

		return true;
	}

	private void CheckColorFilled(int ColorIndex)
	{
		if(jar.PercentageFilled[ColorIndex] >= 1f )
		{
			if(ColorIndex < jar.PercentageFilled.Count - 1)
			{				
				// Call game manager to change to next color
			}
			else
			{
				// All colors are completed, call game manager to end game with victory condition	
			}
		}
	}

	public static Jar CreateTargetSandJar(ColorPalette palette)
	{
		List<Color> remainingColors = new List<Color> (palette.Colors);
		List<Color> result = new List<Color>();

		int layerNumber = Random.Range(remainingColors.Count, MaximumLayerNumber);

		while(remainingColors.Count > 0)
		{
			// Take one reandomly from available and put it on the queue
			int randomIndex = Random.Range(0, remainingColors.Count);
			Color targetColor = remainingColors[randomIndex];
			remainingColors.RemoveAt(randomIndex);
			result.Add(targetColor);

			if(result.Count > 1 && result.Count <= layerNumber)
			{
				// Check if repeating
				if((Random.Range(0.0f,1.0f) > 0.5f))
				{
					for(int i = 0; i < result.Count - 1; i++)
					{
						// Maybe repeat all except the last one
						if((Random.Range(0.0f,1.0f) > 0.5f))
						{
							result.Add(result[i]);
							break;
						}
					}					
				}
			}
		}

		// Assign layer percentages
		List<float> layerAmount = new List<float>();
		float equalAmount = 1f / result.Count;

		for(int i = 0; i < result.Count; i++)
		{
			layerAmount.Add(equalAmount);
		}

		Jar resultJar;
		resultJar.ColorsToFill = result;
		resultJar.PercentageFilled = layerAmount;
		resultJar.ColorsFilled = new Color[result.Count];
		resultJar.currentIndex = 0;

		return resultJar;

	}

	public GameObject CreateTargetSandJarGameObject(Jar jar, Transform parent)
	{
		for (int i = 0; i < jar.ColorsToFill.Count; i++) 
		{
			GameObject layer = Instantiate(sandLayerPrefab);
			layer.GetComponent<Renderer>().material.color = jar.ColorsToFill[i];

			layer.transform.parent = parent;
			layer.transform.localPosition = Vector3.zero;
			layer.transform.position += Vector3.up*layer.transform.localScale.y*2*i;
		}
		return sandLayerPrefab;
	}

	public void AddLayerToPlayerJar(Color color, int index)
	{
		GameObject layer = Instantiate(sandLayerPrefab);
		layer.GetComponent<Renderer>().material.color = color;

		layer.transform.parent = playerSandJarParent.transform;
		layer.transform.localPosition = Vector3.zero;
		layer.transform.position += Vector3.up*layer.transform.localScale.y*2*index;
	}
}
