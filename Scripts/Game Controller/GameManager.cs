using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[HideInInspector]
	public bool gameStartedFromMenu, gameRestarted;

	[HideInInspector]
	public float lives;

	[HideInInspector]
	public float power;


	// Use this for initialization
	void Awake () {
		MakeSingleton ();
	}
	
	void MakeSingleton(){
		if(instance != null){
			Destroy (gameObject);
		}else{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
}
