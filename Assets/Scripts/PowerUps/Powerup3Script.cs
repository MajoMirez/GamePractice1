using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup3Script : PowerupScript
{
	// golpe
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
		LevelManager.Instance.setBuffGolpe(2, 3);
	}
}