using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AudioManager : MonoBehaviour {

	public AudioSource Perc1;
	public AudioSource Perc2;
	public AudioSource Harmony;

	public static AudioManager Instance { get; set; }

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	public void ResetAll()
	{
		DOTween.Kill("a1");
		DOTween.Kill("a2");
		DOTween.Kill("a3");
		Perc1.DOFade(0, 1.5f); 
		Perc2.DOFade(0, 1.5f); 
		Harmony.DOFade(0, 1.5f); 
	}

	public void ActivatePerc1()
	{
		Perc1.DOFade(1, 2).SetId("a1"); 
	}

	public void ActivatePerc2()
	{
		Perc2.DOFade(1, 2).SetId("a2"); 
	}

	public void ActivateHarmony()
	{
		Harmony.DOFade(1, 2).SetId("a3"); 
	}
}
