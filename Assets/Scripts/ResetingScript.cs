using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResetingScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener (TaskOnClick);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		LevelManager.Instance.setPaused (false);
		LevelManager.Instance.setPlaying (false);
		LevelManager.Instance.setScore (0);
		LevelManager.Instance.setVidas (3);
	}
}
