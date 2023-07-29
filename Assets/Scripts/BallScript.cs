using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

	public float veloc = 5f;
	private bool preLaunch = true;
	private Vector3 ballPos;
	private float mousePosInBlocks;
	private GameObject trail_fiery;
	private GameObject trail_icey;
	private GameObject trail_lively;
	private SpriteRenderer ball_SR;// block_SR;
	private Rigidbody2D ball_RB2;
	private Element ballElement;
	private int score = 5, bonus = 10;
	private float buffVelocidad = 1f;
	private float buffGolpe = 1f;

	private void Awake()
	{
		preLaunch = true;
		if (LevelManager.Instance.getNpelotas() >= 1)
		{
			preLaunch = false;
		}
		LevelManager.Instance.addNpelotas(1);
	}

	// Use this for initialization
	void Start()
	{
		ball_RB2 = GetComponent<Rigidbody2D>();
		ball_SR = GetComponent<SpriteRenderer>();

		trail_fiery = this.transform.Find("Fiery_Trail").gameObject;
		trail_icey = this.transform.Find("Icey_Trail").gameObject;
		trail_lively = this.transform.Find("Lively_Trail").gameObject;

		ballElement = PaddleScript.paddleElement;
		ball_SR.color = BrickScript.DicColor[ballElement];
		SetTrailElement();
	}

	// Update is called once per frame
	void Update()
	{
		if (preLaunch)
		{
			if (!LevelManager.Instance.getPaused())
			{//getPaused = false : juego no pausado
				mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;//0..16
				ballPos = new Vector3(0.5f, this.transform.position.y, 0f);
				ballPos.x = Mathf.Clamp(mousePosInBlocks, 1f, 15f);
				ballPos.y = 1f;
				this.transform.position = ballPos;
			}

			if (Input.GetMouseButtonDown(0))
			{
				ball_RB2.velocity = new Vector2(veloc, veloc);
				preLaunch = false;
				LevelManager.Instance.setPlaying(true);
			}
		}
		else
		{
			if (ball_RB2.velocity == Vector2.zero)
			{
				ball_RB2.velocity = new Vector2(veloc, veloc);
			}
			buffVelocidad = LevelManager.Instance.getBuffVelocidad();
			ball_RB2.velocity = ball_RB2.velocity.normalized * veloc * buffVelocidad;

			buffGolpe = LevelManager.Instance.getBuffGolpe();
			this.transform.localScale = Vector3.one * buffGolpe;
			if (buffGolpe > 1)
			{
				AplicarBuffGolpe();
			}
		}
	}

	void SetTrailElement()
	{
		switch (ballElement)
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

	void AplicarBuffGolpe()
	{

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			ballElement = PaddleScript.paddleElement;
			ball_SR.color = BrickScript.DicColor[ballElement];
			SetTrailElement();
		}
		else if (collision.gameObject.tag == "Block")
		{
			DestruirBloque(collision.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (trigger.gameObject.tag == "Block")
		{
			DestruirBloque(trigger.gameObject);
		}
	}

	void DestruirBloque(GameObject gameObject)
	{
		LevelManager.Instance.setLastBrick(gameObject.transform.position);
		LevelManager.Instance.resetLastScore();
		LevelManager.Instance.addScore(score);

		if (gameObject.GetComponent<BrickScript>().brickElement == ballElement)
		{
			LevelManager.Instance.addScore(bonus);
			print("bonus!");
		}

		gameObject.GetComponent<BrickScript>().BeforeDestroying();
		Destroy(gameObject);
		LevelManager.Instance.addTarget(-1);
	}

	public Vector3 getBallPos()
	{
		return this.transform.position;
	}
}
