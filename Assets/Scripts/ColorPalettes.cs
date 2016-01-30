using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ColorPalette 
{
	public Color[] Colors;
};

/// <summary>
/// Contains different color palettes that will be used by different game elements
/// </summary>
public class ColorPalettes : MonoBehaviour {

	public ColorPalette[] Palettes;

	public ColorPalette GetRandomPalette()
	{
		int index = Random.Range(0, Palettes.Length);
		return Palettes[index];
	}

}
