using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[HideInInspector]
	public Text liveText;
	[HideInInspector]
	public float score, lives;
	private Player player;
	private Score scoreScript;
	private Text scoreText2, highScoreText2, pointsText2;

	public int points;
	[HideInInspector]
	public Text pointsText;
	private int highPoint = 0;

	private GameObject pausePanel;

	private Text playerScoreText;


	void Awake () {
		MakeInstance ();

		lives = 3;
		points = 0;
		liveText = GameObject.Find (Tags.LIVES_TEXT).GetComponent<Text> ();
		player = GameObject.Find (Tags.PLAYER_TAG).GetComponent<Player> ();
		pointsText = GameObject.Find (Tags.POINTS_TEXT).GetComponent<Text> ();

		highScoreText2 = GameObject.Find (Tags.HIGH_SCORE_TEXT2).GetComponent<Text> ();
		scoreText2 = GameObject.Find (Tags.SCORE_TEXT2).GetComponent<Text> ();
		pointsText2 = GameObject.Find (Tags.POINTS_TEXT2).GetComponent<Text> ();

		//pausePanel = GameObject.Find (Tags.PAUSE_PANEL);
		pausePanel = GameObject.FindGameObjectWithTag (Tags.PAUSE_PANEL);
		pausePanel.SetActive (false);

		playerScoreText = GameObject.Find (Tags.SCORE_TEXT).GetComponent<Text> ();
	}

	void Start(){
		highPoint = PlayerPrefs.GetInt ("highPoint");
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneWasLoaded;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneWasLoaded;
		instance = null;
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("highPoint", highPoint);
	}

	void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	//We create this scene in other to use delegate
	void OnSceneWasLoaded(Scene scene, LoadSceneMode mode){
		if (scene.name == Tags.GAMEPLAY_SCENE) {
			if (GameManager.instance.gameStartedFromMenu) {
				GameManager.instance.gameStartedFromMenu = false;
				lives = 3;
			}else if(GameManager.instance.gameRestarted){
				GameManager.instance.gameRestarted = false;
				lives = GameManager.instance.lives;
				points = GameManager.instance.points;
				score = GameManager.instance.score;
			}

			liveText.text = "Lives: " + lives.ToString ();
			pointsText.text = "Points: " + points.ToString ();
			playerScoreText.text = "Score: " + Score.score.ToString ();
		}
	}

	public void TakeDamage(){
		lives--;

		if (lives > 0) {
			//health--;
			StartCoroutine (PlayerDied (Tags.GAMEPLAY_SCENE));
			liveText.text = "Lives: " + lives.ToString ();
			pointsText.text = "Points: " + points.ToString ();
			Score.scoreText.text = "Score: " + Score.score.ToString ();
		} else {
			StartCoroutine (WaitBeforeReplay ());
		}
	}

	public void KillEnemy(){
		points++;
		pointsText.text = "Points: " + points.ToString ();
	}

	public IEnumerator PlayerDied(string sceneName){
		GameManager.instance.lives = lives;
		GameManager.instance.points = points;
		GameManager.instance.gameRestarted = true;

		yield return new WaitForSecondsRealtime (3f);
		SceneManager.LoadScene (sceneName);
	}

	public IEnumerator WaitBeforeReplay(){
		yield return new WaitForSecondsRealtime (2f);
		liveText.text = "Lives: " + 0;
		player.gameOver.SetActive (true);

		scoreText2.text = "Score: " + Score.score.ToString ();
		highScoreText2.text = "High Score: " + Score.highScore.ToString ();

		if (points > highPoint) {
			highPoint = points;
		}

		pointsText2.text = "Points: " + highPoint.ToString ();

	}

	public void PlayAgain(){
		Time.timeScale = 1f;
		SceneManager.LoadScene (Tags.GAMEPLAY_SCENE);
		Score.score = 0;
		points = 0;
	}

	public void PauseGame(){
		pausePanel.SetActive (true);
		Time.timeScale = 0f;
	}

	public void ResumeGame(){
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

	public void QuitGame(){
		Application.Quit ();
	}


}






















