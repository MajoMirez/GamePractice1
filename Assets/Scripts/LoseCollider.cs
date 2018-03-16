using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	public GameObject gameOverCanvas;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D trigger ){
		print ("Game Over!");
		//levelManager.LoadLevel ("Lose Screen");
		gameOverCanvas.SetActive(true);
		LevelManager.Instance.setPaused (true);
		LevelManager.Instance.setPlaying (false);
		LevelManager.Instance.setScore (0);
	}



}