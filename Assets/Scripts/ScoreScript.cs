using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public Text texto;
	private int score;
	//public Text tutorial;

	// Use this for initialization
	void Start () {
		//tutorial.text = "Breaking brick with the same color " 
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelManager.Instance.getPlaying()){
			score = LevelManager.Instance.getScore ();
			texto.text = "Score: "+score.ToString();
		}

	}
}
