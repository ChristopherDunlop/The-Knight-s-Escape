using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	public GameObject mop,level;
	private GameObject levelObj, playerObj;
	public bool levelComplete = false;
	public int toMop, mopped, maxX, maxY;
	
	void Start () {
		Instantiate(level, new Vector3 (0f,0f,0f), Quaternion.identity);
		//levelObj = GameObject.Find("Level 0(Clone)");
		//levelObj.name=("Level");

		Instantiate(mop, new Vector3 (0f,0f,0f), Quaternion.identity);
		playerObj = GameObject.Find("Player(Clone)");
		playerObj.name=("Player");
	}
	
	void Update () {

		if (mopped >= toMop) {
			levelComplete = true;
		}
	}
}
