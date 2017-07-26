using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	void FixedUpdate(){
		if(Input.GetMouseButtonDown (0)){
			GameManager.instance.gameStartedFromMenu = true;
			SceneManager.LoadScene (Tags.GAMEPLAY_SCENE);
		}
	}

	public void PlayGame(){
		GameManager.instance.gameStartedFromMenu = true;
		SceneManager.LoadScene (Tags.GAMEPLAY_SCENE);
	}
}
