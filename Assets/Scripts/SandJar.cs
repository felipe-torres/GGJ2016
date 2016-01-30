using UnityEngine;
using System.Collections;

/// <summary>
/// Will contain data on the sand jar completeness (color and completeness percentage)
/// </summary>
public class SandJar : MonoBehaviour {

	private Color[] ColorsToFill;
	private float[] PercentageFilled;

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
		print("Jar filled at: "+ColorIndex+" , with valur of: "+ value);
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
}
