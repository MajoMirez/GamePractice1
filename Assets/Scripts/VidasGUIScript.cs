using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasGUIScript : MonoBehaviour
{
	public Text texto;
	private int vidas;

	// Use this for initialization
	void Start()
	{
		//tutorial.text = "Breaking brick with the same color " 
	}

	// Update is called once per frame
	void Update()
	{

		vidas = LevelManager.Instance.getVidas();
		texto.text = "Vidas: " + vidas.ToString();
	}
}
