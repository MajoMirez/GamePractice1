using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

	public static LevelManager Instance;
	public GameObject winPrefab;
	public GameObject pausePrefab;
	public GameObject gameOverPrefab;
	private GameObject winCanvas;
	private GameObject pauseCanvas;
	private GameObject gameOverCanvas;
	public GameObject ballPrefab;
	public GameObject unorojo;
	private int score = 0;
	private int lastScore = 0;
	private Vector3 lastBrick = new Vector3(0, 0, 0);
	private bool paused = false;
	private bool playing = false; //se esta jugando
	public int vidas = 3;
	private int ultimaVidaGanada = 0;
	private int Npelotas = 0; //numero pelotas en juego
	private int target; // Cantidad de bloques en pantalla
	private float buffVelocidad = 1f;
	private float TbuffVelocidad = 0f;
	private float buffGolpe = 1f;
	private float TbuffGolpe = 0f;

	void Awake()
	{
		int ID = GetInstanceID();
		Debug.Log("LevelManager ID" + ID + " Awake");
		DontDestroyOnLoad(this.gameObject);
		if (Instance == null)
		{
			Instance = this; //El unico manager del juego
		}
		else
		{
			Destroy(this.gameObject); //Si ya existe otro, se destruye
			Debug.Log("LevelManager ID" + ID + " selfdestructed");
		}
	}

	void Start()
	{
		target = GameObject.FindGameObjectsWithTag("Block").Length - 1;

		LevelManager.Instance.cargarNivel();
	}

	void Update()
	{

		if (LevelManager.Instance.getPlaying())
		{
			target = GameObject.FindGameObjectsWithTag("Block").Length - 1;
			Time.timeScale = paused ? 0 : 1;
			if (target < 1)
			{ //WIN

				winCanvas.SetActive(true);
				pauseCanvas.SetActive(false);
				LevelManager.Instance.setPaused(true);
				LevelManager.Instance.setPlaying(false);
			}

			/*La pausa funciona cuando se apreta P y se esta jugando */
			if (Input.GetKeyDown(KeyCode.P)
				&& LevelManager.Instance.getPlaying())
			{ //PAUSE
				paused = LevelManager.Instance.getPaused();
				pauseCanvas.SetActive(!paused);
				LevelManager.Instance.setPaused(!paused);
			}
			revisarBuffs();
		}
	}

	void revisarBuffs()
	{
		if (TbuffVelocidad > 0)
		{
			TbuffVelocidad -= Time.deltaTime;
			if (TbuffVelocidad <= 0)
			{
				buffVelocidad = 1;
			}
		}
		if (TbuffGolpe > 0)
		{
			TbuffGolpe -= Time.deltaTime;
			if (TbuffGolpe <= 0)
			{
				buffGolpe = 1f;
			}
		}
	}

	public bool aplicaDano()
	{
		bool pierdeVida = false;
		LevelManager.Instance.addNpelotas(-1);
		if (LevelManager.Instance.getNpelotas() <= 0)
		{
			pierdeVida = true;
			LevelManager.Instance.addVidas(-1);

			if (LevelManager.Instance.getVidas() == 0)
			{
				waiter(2);
				gameOverCanvas.SetActive(true);
			}
			else
			{
				LevelManager.Instance.setPlaying(false);
				LevelManager.Instance.NewBall();
			}
		}
		return pierdeVida;
	}

	public void NewBall()
	{
		Instantiate(ballPrefab);
	}

	IEnumerator waiter(int t)
	{
		yield return new WaitForSeconds(t);
		LevelManager.Instance.setPlaying(false);
		LevelManager.Instance.setPaused(true);
	}

	public void cargarNivel()
    {
		pauseCanvas=Instantiate(pausePrefab);
		pauseCanvas.SetActive(false);
		winCanvas=Instantiate(winPrefab);
		winCanvas.SetActive(false);
		gameOverCanvas=Instantiate(gameOverPrefab);
		gameOverCanvas.SetActive(false);
		target = GameObject.FindGameObjectsWithTag("Block").Length - 1;
        if (target > 0)
        {
			LevelManager.Instance.setPlaying(true);
        }
	}

	#region Getters and Setters
	public int getScore()
	{
		return score;
	}

	public void addScore(int s)
	{
		score = score + s;
		lastScore += s;
	}

	public void setScore(int s)
	{
		score = s;
	}

	public bool getPaused()
	{
		return paused;
	}

	public void setPaused(bool v)
	{
		paused = v;
		print("paused: " + paused.ToString() + "\nPlaying: " + playing.ToString());
	}

	public bool getPlaying()
	{
		return playing;
	}

	public void setPlaying(bool v)
	{
		playing = v;
		print("paused: " + paused.ToString() + "\nPlaying: " + playing.ToString());
	}

	public int getVidas()
	{
		return vidas;
	}

	public void setVidas(int i)
	{
		vidas = i;
	}

	public void addVidas(int i)
	{
		ultimaVidaGanada = i;
		vidas = vidas + i;
		Instantiate(unorojo);
	}

	public int getUltimaVidaGanada()
	{
		return ultimaVidaGanada;
	}

	public int getLastScore()
	{
		return lastScore;
	}

	public void resetLastScore()
	{
		lastScore = 0;
	}

	public void setLastBrick(Vector3 v)
	{
		lastBrick = v;
	}

	public Vector3 getLastBrick()
	{
		return lastBrick;
	}

	public int getNpelotas()
	{
		return Npelotas;
	}

	public void addNpelotas(int n)
	{
		Npelotas += n;
		print("pelotas en juego:" + Npelotas.ToString());
	}

	public void setNpelotas(int n)
	{//por si lo llegase a necesitar..
		Npelotas = n;
		print("pelotas en juego:" + Npelotas.ToString());
	}

	public void addTarget(int n)
	{
		target += n;
	}

	public float getBuffVelocidad()
	{
		return buffVelocidad;
	}

	public void setBuffVelocidad(float i, float t)
	{
		buffVelocidad = i;
		TbuffVelocidad = t;
	}
	public float getBuffGolpe()
	{
		return buffGolpe;
	}

	public void setBuffGolpe(float i, float t)
	{
		buffGolpe = i;
		TbuffGolpe = t;
	}
	#endregion

	#region Level Management
	public void LoadLevel(string levelName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		//LevelManager.Instance.Start();
		//LevelManager.Instance.cargarNivel();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
	#endregion
}