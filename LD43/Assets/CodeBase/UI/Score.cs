using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour 
{
	public GameObject ScorePrefab;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateScores();
	}

	void UpdateScores()
	{
		for (int i = transform.childCount; i > 0; i--)
		{
			Destroy(transform.GetChild(i -1).gameObject);
		}

		foreach(Player p in FindObjectsOfType<Player>())
		{
			GameObject PlayerScore = Instantiate<GameObject>(ScorePrefab, transform);
			PlayerScore.GetComponent<PlayerScore>().PlayerName.text = p.Name;
			PlayerScore.GetComponent<PlayerScore>().Score.text = p.Score.ToString();
		}
	}
}
