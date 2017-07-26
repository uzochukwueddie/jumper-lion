using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static Score instance;

	[HideInInspector]
	public static int score = 0, highScore = 0;
	[HideInInspector]
	public static Text scoreText, highScoreText;
	[HideInInspector]
	public Text highScoreText2, scoreText2;

	// Use this for initialization
	void Start () {
		ScoreInstance ();
		scoreText = GameObject.Find (Tags.SCORE_TEXT).GetComponent<Text> ();
		//highScoreText = GameObject.Find (Tags.HIGH_SCORE_TEXT).GetComponent<Text> ();
		//highScoreText2 = GameObject.Find (Tags.HIGH_SCORE_TEXT2).GetComponent<Text> ();
		//scoreText2 = GameObject.Find (Tags.SCORE_TEXT2).GetComponent<Text> ();

		highScore = PlayerPrefs.GetInt ("highScore");
	}

	void ScoreInstance(){
		if (instance == null) {
			instance = this;
		}
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("highScore", highScore);
	}

	public static void IncrementScore(){
		score++;

		scoreText.text = "Score: " + score.ToString ();
		//highScoreText.text = "High Score: " + highScore.ToString ();

		if (score > highScore) {
			highScore = score;
		}
	}
}
