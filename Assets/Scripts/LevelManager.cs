using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance;
	private int score=0;
	private bool paused=false;
	private bool playing=false; //se esta jugando

	void Awake(){
		int ID = GetInstanceID ();
		Debug.Log ("LevelManager ID"+ID+" Awake");

		DontDestroyOnLoad (this.gameObject);
		if (Instance == null) {
			Instance = this; //El unico manager del juego
		} else {
			Destroy (this.gameObject); //Si ya existe otro, se destruye
			Debug.Log("LevelManager ID"+ID+" selfdestructed");
		}
	
	}
		
	void Start () {

		//Debug.Log("LevelManager ID"+GetInstanceID()+" Start");
	}

	void Update(){
		print ("paused: "+paused.ToString()+"\nPlaying: "+playing.ToString());
		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
		



	#region Getters and Setters
	public int getScore(){
		return score;
	}

	public void addScore(int s){
		score = score + s;
	}

	public void setScore(int s){
		score = s;
	}

	public bool getPaused(){
		return paused;
	}

	public void setPaused(bool v){
		paused = v;
	}

	public bool getPlaying(){
		return playing;
	}

	public void setPlaying(bool v){
		playing = v;
	}
	#endregion

	#region Level Management
	public void LoadLevel(string levelName){
		UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
	}

	public void QuitGame(){
		Application.Quit ();
	}
	#endregion
}


/*

public class MiManager : MonoBehaviour {

	public static MiManager Instance;
	public MiAudio miAudio;
	public MiScore miScore;
	public int monedas = 0;

	void Start () {
		DontDestroyOnLoad (this.gameObject);
		if (Instance == null) {
			Instance = this; //El unico manager del juego
		} else {
			Destroy (this.gameObject); //Si ya existe otro, se destruye
		}
	}

	public string nombreEscena;

	public void CambiarEscena (){
		UnityEngine.SceneManagement.SceneManager.LoadScene (nombreEscena);
	}
	
	public void AddMonedas(int cant){
		if ((monedas + cant) >= 0) {
			monedas += cant;
		} else {
			monedas = 0;
		}
	}

	public int getMonedas(){
		return monedas;
	}
}*/