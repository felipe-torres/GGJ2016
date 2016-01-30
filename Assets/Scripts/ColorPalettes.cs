using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ColorPalette 
{
	public Color[] Colors;
};

public class ColorPalettes : MonoBehaviour {

	public ColorPalette[] Palettes;

	public ColorPalette GetRandomPalette()
	{
		int index = Random.Range(0, Palettes.Length);
		return Palettes[index];
	}

}
