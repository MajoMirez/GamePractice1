using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup2Script : PowerupScript
{
	//veloc
	// Use this for initialization
	void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	void Update()
	{
		base.Update();
	}

	public override void PUeffect()
	{
		LevelManager.Instance.setBuffVelocidad(3, 2);
	}
}
