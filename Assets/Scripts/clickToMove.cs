using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class clickToMove : MonoBehaviour {

	// Use this for initialization
	public float speed = 7f;
	private Vector3 target;
	public int movement = 0;
	Vector3 playerPos;
	Rigidbody2D player;
	public List<Vector3> posList = new List<Vector3>();
	public GameObject mopBackground, lava1, lava2;
	GameObject gc, smooth, moppedSquares;
	gameController levelScript;

	void Start () {
		target = transform.position;
		player = GetComponent<Rigidbody2D> ();

		gc = GameObject.Find("GameController");		
		levelScript = gc.GetComponent<gameController>();

		smooth = new GameObject();
		smooth.name=("Fluid Motion");
		moppedSquares = new GameObject ();
		moppedSquares.name = ("Lava");
		player = GetComponent<Rigidbody2D> ();		
		playerPos = player.transform.position;
		float x2 = Mathf.Round(playerPos.x*100);
		float y2 = Mathf.Round(playerPos.y*100);
		
		playerPos.Set((x2)/100,(y2)/100,0);
		//posList.Add (playerPos);
		Instantiate (mopBackground, (playerPos), Quaternion.identity);
		
		GameObject newBackground = GameObject.Find("Background(Clone)");
		newBackground.name=("Background " + playerPos);
		newBackground.transform.parent = smooth.transform;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0) && movement == 0) {
			target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			target.z = transform.position.z;

			playerPos = player.transform.position;
			float x = Mathf.Round (playerPos.x * 100);
			float y = Mathf.Round (playerPos.y * 100);
			float x2 = Mathf.Round (target.x * 100);
			float y2 = Mathf.Round (target.y * 100);

			if (x2 >= 0 && y2 >= 0 && x2 < levelScript.maxX && y2 < levelScript.maxY)
			{

			x2 = x2 - (x2 % 128);
			y2 = y2 - (y2 % 128);

			target.Set(x2/100,y2/100,0);

			if((Mathf.Abs(x - x2) ==  256 && Mathf.Abs(y - y2) == 128) || (Mathf.Abs(x - x2) ==  128 && Mathf.Abs(y - y2) == 256)){
				if (!posList.Contains(target)) {
						movement = 1;
						posList.Add (playerPos);
						Instantiate (lava1, (playerPos), Quaternion.identity);
						GameObject newMopped = GameObject.Find("lava1(Clone)");
						newMopped.name=("Lava1 " + playerPos);
						newMopped.transform.parent = moppedSquares.transform;
						
						gc = GameObject.Find("GameController");		
						levelScript = gc.GetComponent<gameController>();
						levelScript.mopped++;
				}
			
			}
			}
		
		}
		if (movement == 1) {
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		}


		if (player.transform.position == target){			
			movement=0;
				if (player.transform.position != playerPos){
				Instantiate (lava2, (playerPos), Quaternion.identity);
				GameObject newMopped = GameObject.Find("lava2(Clone)");
				newMopped.name=("Lava2 " + playerPos);
				newMopped.transform.parent = moppedSquares.transform;
				
				gc = GameObject.Find("GameController");		
				levelScript = gc.GetComponent<gameController>();
				}

			player = GetComponent<Rigidbody2D> ();
			playerPos = player.transform.position;
			
			float x = Mathf.Round(playerPos.x*100);
			float y = Mathf.Round(playerPos.y*100);
			
			x = x - (x % 128);
			y = y - (y % 128);
			playerPos.Set(x/100,y/100,0);
			
			GetComponent<Rigidbody2D> ().transform.position = playerPos;
		}
	}    
}
