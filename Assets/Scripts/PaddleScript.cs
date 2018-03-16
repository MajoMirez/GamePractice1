using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {


	private Color fiery = new Color (1.0f,0.8f,0.1f); 
	private Color lively= new Color (0.4f,1.0f,0.0f); 
	private Color icey  = new Color (0.4f,0.5f,1.0f);	
	private GameObject trail_fiery;
	private GameObject trail_icey;
	private GameObject trail_lively;

	private SpriteRenderer SR;
	private Vector3 paddlePos;
	private float mousePosInBlocks;
	private bool flag; //para ver si el paddle se puede mover

	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer>();
		trail_fiery = this.transform.Find ("Fiery_Trail").gameObject;
		trail_icey  = this.transform.Find ("Icey_Trail").gameObject;
		trail_lively= this.transform.Find ("Lively_Trail").gameObject;
	}
		
	// Update is called once per frame
	void Update () {

		if (!LevelManager.Instance.getPaused()) {//getPaused = false : juego no pausado
			mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;//0..16
			paddlePos = new Vector3 (0.5f, this.transform.position.y, 0f);
			paddlePos.x = Mathf.Clamp (mousePosInBlocks, 1f, 15f);
			this.transform.position = paddlePos;

			if (Input.GetMouseButtonDown (0) && LevelManager.Instance.getPlaying()) {
				if (SR.color == icey) {
					SR.color = lively;
					trail_icey.SetActive (false);
					trail_lively.SetActive (true);

				} else if (SR.color == lively) {
					SR.color = fiery;
					trail_lively.SetActive (false);
					trail_fiery.SetActive (true);

				} else if (SR.color == fiery) {
					SR.color = icey;
					trail_fiery.SetActive (false);
					trail_icey.SetActive (true);

				} else {
					SR.color = lively;
					trail_lively.SetActive (true);
				}
			}
		}
	}
		
	#region Getters
	public Color getColor_Fiery(){
		return fiery;
	}

	public Color getColor_Icey(){
		return icey;
	}

	public Color getColor_Lively(){
		return lively;
	}
	#endregion
}
