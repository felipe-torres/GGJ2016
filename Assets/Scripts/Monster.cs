using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Monster : MonoBehaviour {

	public Transform Player;

	// Use this for initialization
	void Start () {
		MonsterAdvanceForward();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MonsterAdvanceForward() 
	{
		Vector3 direction = Player.position - transform.position;
		direction.Normalize();
		transform.DOMove(direction, 40f);

	}
}
