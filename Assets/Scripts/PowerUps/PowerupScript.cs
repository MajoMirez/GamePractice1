using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

	public float caida=0.05f;
	float t=0;
	// Use this for initialization
	public void Start () {
		this.transform.position = LevelManager.Instance.getLastBrick ();
	}
	
	// Update is called once per frame
	public void Update () {
		this.transform.Translate (0, -caida, 0);
		t += Time.deltaTime;
		if (t > 5) {
			Destroy(this.gameObject);
		}
	}

	public virtual void PUeffect(){
		print ("PUefect  powerup 0");
	}
}
