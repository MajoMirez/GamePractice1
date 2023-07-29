using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {Icy,Lively,Fiery};

public class BrickScript : MonoBehaviour
{
	public static Dictionary<Element, Color> DicColor = new Dictionary<Element, Color>{
		{Element.Icy, new Color (0.6f,1.0f,1.0f)},
		{Element.Lively, new Color (0.3f,1.0f,0.1f)},
		{Element.Fiery, new Color (1.0f,0.4f,0.0f)}
	};

	public Element brickElement;
	public GameObject powerUp;
	public GameObject superPowerUp;
	public GameObject puntajeTxt;
	private ParticleSystem PS;
	private Collider2D paddle_C2;
	static System.Random a = new System.Random();
	int b;
	int PUChancePerBrick = 100;
	private float buffGolpe = 1f;

	// Use this for initialization
	void Start()
	{
		PS = GetComponent<ParticleSystem>();
		paddle_C2 = GetComponent<Collider2D>();
		var emision = PS.emission;
		emision.enabled = false;

		b = a.Next(100);
		if (b <= PUChancePerBrick)
		{
			emision.enabled = true;
		}
	}

	void Update()
	{
		buffGolpe = LevelManager.Instance.getBuffGolpe();
		if (buffGolpe > 1)
		{
			paddle_C2.isTrigger = true;
		}
		else
		{
			paddle_C2.isTrigger = false;
		}
	}

	public void BeforeDestroying()
	{
		Instantiate(puntajeTxt);
		if (PS.emission.enabled)
		{
			b = a.Next(100);
			if (b <= 20)
			{
				Instantiate(superPowerUp);
			}
			else
			{
				Instantiate(powerUp);
			}
		}
	}
}