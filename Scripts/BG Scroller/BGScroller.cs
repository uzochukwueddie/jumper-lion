using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

	public float offsetSpeed = -0.0006f;

	[HideInInspector]
	public bool canScroll;

	[HideInInspector]
	public MeshRenderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canScroll) {
			myRenderer.material.mainTextureOffset -= new Vector2 (offsetSpeed * Time.deltaTime, 0);
		}
	}

	IEnumerator ScrollBG(){
		yield return new WaitForSeconds (2f);
		myRenderer.material.mainTextureOffset -= new Vector2 (offsetSpeed * Time.deltaTime, 0);
	}
}
