using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {

	public GameObject[] pipePrefab;
	public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 0 && Player.instance.playerDied == false) {
			Spawner ();
		}
		timer -= Time.deltaTime;
	}

	void Spawner(){
		int index = Random.Range(0, pipePrefab.Length);
		Instantiate (pipePrefab[index], new Vector3 (19.5f, 0, 10f), Quaternion.Euler(0, 0, 0));
		timer = 5f;
	}
}
