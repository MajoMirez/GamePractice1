using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LevelManager.Instance.setPaused (false);
		LevelManager.Instance.setPlaying (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
