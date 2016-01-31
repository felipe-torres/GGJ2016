using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Will contain data on the sand jar completeness (color and completeness percentage)
/// </summary>
public class SandJar : MonoBehaviour {

	private Color[] ColorsToFill;
	private float[] PercentageFilled;

	public int MaximumLayerNumber = 10;

	// Use this for initialization
	void Start () {
	
	}

	public void Initialize(ColorPalette palette)
	{
		ColorsToFill = new Color[palette.Colors.Length];
		PercentageFilled = new float[ColorsToFill.Length];
	}

	public void Fill(int ColorIndex, float value)
	{
		print("Jar filled at: "+ColorIndex+" , with value of: "+ value);
		PercentageFilled[ColorIndex] += value;

		CheckColorFilled(ColorIndex);
	}

	private void CheckColorFilled(int ColorIndex)
	{
		if(PercentageFilled[ColorIndex] >= 1f )
		{
			if(ColorIndex < PercentageFilled.Length - 1)
			{				
				// Call game manager to change to next color
			}
			else
			{
				// All colors are completed, call game manager to end game with victory condition	
			}
		}
	}

	public static void CreateTargetSandJar(ColorPalette palette)
	{
		List<Color> remainingColors = new List<Color> (palette.Colors);
		List<Color> result = new List<Color>();

		//int layerNumber = Random.Range(remainingColors.Count, MaximumLayerNumber);

		while(remainingColors.Count > 0)
		{
			// Take one reandomly from available and put it on the queue
			int randomIndex = Random.Range(0, remainingColors.Count);
			Color targetColor = remainingColors[randomIndex];
			remainingColors.RemoveAt(randomIndex);
			result.Add(targetColor);

			if(result.Count > 1)
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

		for(int i = 0; i < result.Count; i++)
		{
			print(result[i]);
		}	

	}
}
