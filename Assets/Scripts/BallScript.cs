using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	public PaddleScript paddle;
	public GameObject winCanvas;
	public GameObject pauseCanvas;
	public float veloc=5f;
	private int target; // Cantidad de bloques en pantalla
	private bool flag=true; //la bola esta en el paddle
	private Vector3 paddleToBallVector;
	private bool paused=false;

	private Color fiery, lively, icey;	
	private GameObject trail_fiery;
	private GameObject trail_icey;
	private GameObject trail_lively;
	private SpriteRenderer ball_SR, pad_SR, block_SR;
	private Rigidbody2D ball_RB2;
	private int score=5, bonus=10;

	// Use this for initialization
	void Start () {
		paddleToBallVector = this.transform.position - paddle.transform.position;
		target = GameObject.FindGameObjectsWithTag ("Block").Length-1;

		ball_RB2 = GetComponent<Rigidbody2D>();
		ball_SR = GetComponent<SpriteRenderer>();
		pad_SR = paddle.GetComponent<SpriteRenderer>();

		trail_fiery = this.transform.Find ("Fiery_Trail").gameObject;
		trail_icey  = this.transform.Find ("Icey_Trail").gameObject;
		trail_lively= this.transform.Find ("Lively_Trail").gameObject;

		fiery = paddle.getColor_Fiery();
		icey  = paddle.getColor_Icey ();
		lively= paddle.getColor_Lively();
	}
	
	// Update is called once per frame
	void Update () {
		if (flag){ //Pre-Lauch
			LevelManager.Instance.setPaused(false);
			this.transform.position = paddle.transform.position + paddleToBallVector;
			if (Input.GetMouseButtonDown (0)) {
				ball_RB2.velocity = new Vector2 (veloc, veloc);
				flag = false;
				LevelManager.Instance.setPlaying (true);
			}
		}//End launch
		if (target < 1) { //WIN
			winCanvas.SetActive (true);
			pauseCanvas.SetActive (false);
			LevelManager.Instance.setPaused (true);
			LevelManager.Instance.setPlaying (false);
		}

		/*La pausa funciona cuando se apreta P
		 * y se esta jugando 
		*/
		if (Input.GetKeyDown(KeyCode.P) && LevelManager.Instance.getPlaying()){ //PAUSE
			paused= LevelManager.Instance.getPaused();
			pauseCanvas.SetActive(!paused);
			LevelManager.Instance.setPaused(!paused);
		}
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			ball_SR.color = pad_SR.color;
			if (ball_SR.color == fiery) {
				trail_lively.SetActive (false);
				trail_fiery.SetActive (true);
				trail_icey.SetActive (false);
			} else if (ball_SR.color == icey) {
				trail_lively.SetActive (false);
				trail_fiery.SetActive (false);
				trail_icey.SetActive (true);
			} else if (ball_SR.color == lively) {
				trail_lively.SetActive (true);
				trail_fiery.SetActive (false);
				trail_icey.SetActive (false);
			}

		} else if (collision.gameObject.tag == "Block") {
			block_SR= collision.gameObject.GetComponent<SpriteRenderer> ();
			if (block_SR.color == ball_SR.color) {
				LevelManager.Instance.addScore (bonus);
				//print ("bonus!");
			}
			Destroy (collision.gameObject);
			LevelManager.Instance.addScore (score);
			target -= 1;

		}
	}


	public int getTarget(){
		return target;
	}
		
}
