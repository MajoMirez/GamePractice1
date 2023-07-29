using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.CompareTag("BALL"))
		{
			LevelManager.Instance.aplicaDano();
			Destroy(trigger.gameObject);
		}
		else if (trigger.gameObject.CompareTag("PowerUp"))
		{
			Destroy(trigger.gameObject);
		}
	}
}