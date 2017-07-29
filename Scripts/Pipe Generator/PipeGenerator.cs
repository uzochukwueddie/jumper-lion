using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour {

	public static PipeGenerator instance;

	[SerializeField]
	public int levelLength = 100;

	[SerializeField]
	private Transform[] pipeUp;

	[SerializeField]
	public Transform pipeParentUp;//, pipeParentDown;

	//[SerializeField]
	//private float distance_between_pipes = 15f;

	[SerializeField]
	private float chanceForEnemyExistence = 0.1f;

	[SerializeField]
	public Transform[] enemy;

	[SerializeField]
	public Transform enemyParent;

	private float pipePositionX;

	[HideInInspector]
	public Transform[] pipeUp_Array;

	[HideInInspector]
	public bool canSpawn;

	public float timer;

	[SerializeField]
	private float minY = -2.73f, maxY = 6f;


	// Use this for initialization
	void Start () {
		//CreateUpPipes ();
		//CreateDownPipes ();
		MakeSingleton();

		canSpawn = true;
	}

	void MakeSingleton(){
		if (instance == null) {
			instance = this;
		}
	}

	/*void CreateUpPipes () {
		pipeUp_Array = new Transform[levelLength];
	
		for(int i = 0; i < pipeUp_Array.Length; i++){
			int index = Random.Range(0, pipeUp.Length);

			Transform newPipeUp = (Transform)Instantiate (pipeUp[index], new Vector3 (10.5f, 0, 0), Quaternion.Euler(0, 0, 0));
			pipeUp_Array [i] = newPipeUp;

			Vector3 pipePositionUp;

			pipePositionUp = new Vector3 ((distance_between_pipes * i)+10.5f, pipeUp[index].position.y, 0);

			pipeUp_Array [i].position = pipePositionUp;
			pipeUp_Array [i].parent = pipeParentUp;

			//SpawnEnemy (pipePositionUp, i, true);
		}
	}*/

	void FixedUpdate(){
		if (timer <= 0 && Player.instance.playerDied == false) {
			SpawnEnemy (true);
		}
		timer -= Time.deltaTime;
	}

	void SpawnEnemy(bool gameStarted){
		if(Random.Range (0f, 1f) < chanceForEnemyExistence){
			if (gameStarted) {
				Vector3 pipePosition = new Vector3 (17.5f, Random.Range (minY, maxY), 99f);
	
				int index = Random.Range (0, enemy.Length);
				Transform createEnemy = (Transform)Instantiate (enemy [index], pipePosition, Quaternion.Euler (180f, 0f, 180f));
				createEnemy.parent = enemyParent;

				timer = 5f;
			}

		}
	}


}







































