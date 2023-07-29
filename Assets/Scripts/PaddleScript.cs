using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
	private GameObject trail_fiery;
	private GameObject trail_icey;
	private GameObject trail_lively;

	private SpriteRenderer SR;
	private Vector3 paddlePos;
	private float mousePosInBlocks;
	public Element iniPaddleElement = Element.Lively;
	public static Element paddleElement;

	// Use this for initialization
	void Awake()
	{
		paddleElement = iniPaddleElement;
		LevelManager.Instance.cargarNivel();
	}

	void Start()
	{
		SR = GetComponent<SpriteRenderer>();
		trail_fiery = this.transform.Find("Fiery_Trail").gameObject;
		trail_icey = this.transform.Find("Icey_Trail").gameObject;
		trail_lively = this.transform.Find("Lively_Trail").gameObject;
		SetTrailElement();
	}

	// Update is called once per frame
	void Update()
	{
		if (!LevelManager.Instance.getPaused())
		{//getPaused = false : juego no pausado
			mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;//0..16
			paddlePos = new Vector3(0.5f, this.transform.position.y, 0f);
			paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1f, 15f);
			this.transform.position = paddlePos;

			if (Input.GetMouseButtonDown(0) & LevelManager.Instance.getPlaying())
			{
				SetNextElement();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "PowerUp")
		{
			trigger.gameObject.GetComponent<PowerupScript>().PUeffect();
			Destroy(trigger.gameObject);
		}
	}

	void SetNextElement()
	{ //Rota elemento del paddle
		switch (paddleElement)
		{
			case Element.Fiery:
				paddleElement = Element.Icy;
				break;
			case Element.Icy:
				paddleElement = Element.Lively;
				break;
			case Element.Lively:
				paddleElement = Element.Fiery;
				break;
		}
		SetTrailElement();
	}

	void SetTrailElement()
	{
		SR.color = BrickScript.DicColor[paddleElement];
		switch (paddleElement)
		{
			case Element.Fiery:
				trail_fiery.SetActive(true);
				trail_icey.SetActive(false);
				trail_lively.SetActive(false);
				break;
			case Element.Icy:
				trail_fiery.SetActive(false);
				trail_icey.SetActive(true);
				trail_lively.SetActive(false);
				break;
			case Element.Lively:
				trail_fiery.SetActive(false);
				trail_icey.SetActive(false);
				trail_lively.SetActive(true);
				break;
		}
	}
}
