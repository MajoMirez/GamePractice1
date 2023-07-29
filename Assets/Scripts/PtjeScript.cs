using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtjeScript : MonoBehaviour {
	public TextMesh text_TM;
	float i=0f;
	float t=2f;
	float alfa=1f;
	int scoreT;
	public Color color;
	// Use this for initialization
	void Start () {
		text_TM = GetComponent<TextMesh> ();
		scoreT = LevelManager.Instance.getLastScore ();
		text_TM.text = "+"+scoreT.ToString();
		this.transform.position = LevelManager.Instance.getLastBrick ();
		//this.transform.Translate(0,0,-1f);

		if (LevelManager.Instance.getLastScore() > 6) {
			text_TM.color = color;
			text_TM.fontSize += 10;
			text_TM.fontStyle = FontStyle.Bold;
		}
	}
	
	// Update is called once per frame
	void Update () {
		i += Time.deltaTime;
		this.transform.Translate (0,0.005f,0);
		alfa -= 0.02f;
		text_TM.color = new Color (text_TM.color.r, text_TM.color.g, text_TM.color.b,alfa );
		if (i >= t) {
			Destroy (this.gameObject);
		}
	}
}
