using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoRojoScript : MonoBehaviour
{
	public TextMesh text_TM;
	float i = 0f;
	float t = 2f;
	float alfa = 1f;
	public Color colorPerdida;
	public Color colorGana;
	Vector3 pos = new Vector3(15.4f, 1.2f, 0f);
	// Use this for initialization
	void Start()
	{
		text_TM = GetComponent<TextMesh>();
		int valor = LevelManager.Instance.getUltimaVidaGanada();
		if (valor >= 0)
		{
			text_TM.text = "+" + valor;
			text_TM.color = colorGana;
		}
		else
		{
			text_TM.text = valor.ToString();
			text_TM.color = colorPerdida;
		}
		gameObject.transform.position = pos;
	}

	// Update is called once per frame
	void Update()
	{
		i += Time.deltaTime;
		this.transform.Translate(0, 0.003f / i, 0);
		alfa -= 0.01f * i;
		text_TM.color = new Color(text_TM.color.r, text_TM.color.g, text_TM.color.b, alfa);
		if (i >= t)
		{
			Destroy(this.gameObject);
		}
	}
}
