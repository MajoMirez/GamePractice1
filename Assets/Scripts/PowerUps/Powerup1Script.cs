using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup1Script : PowerupScript
{
	// +1 pelota

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
		LevelManager.Instance.NewBall();
	}
}
