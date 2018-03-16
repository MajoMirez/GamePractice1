using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public static MusicManager Instance;

	void Awake(){
		int ID = GetInstanceID ();
		Debug.Log ("MusicManager ID"+ID+" Awake");

		DontDestroyOnLoad (this.gameObject);
		if (Instance == null) {
			Instance = this; //El unico manager del juego
		} else {
			Destroy (this.gameObject); //Si ya existe otro, se destruye
			Debug.Log("MusicManager ID"+ID+" selfdestructed");
		}

	}

	void Start () {
		Debug.Log("MusicManager ID"+GetInstanceID()+" Start");
	}

}
