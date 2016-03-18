using UnityEngine;
using System.Collections;

public class levelGenerator : MonoBehaviour {

	public GameObject gameController, gem1, gem2, gem3, gem4;
	public int mops = 0;
	public int maxX, maxY;

	// Use this for initialization
	void Start () {
		GameObject Map = new GameObject();
		Map.name=("Map Components");

		System.Random random = new System.Random();
		int randomX = random.Next(2, 5);
		int randomY = random.Next(2, 5);

		randomX = randomX * 128;
		randomY = randomY * 128;

		maxX = randomX;
		maxY = randomY;

		GameObject mc = GameObject.Find("Main Camera");
		Vector3 cameraPos = new Vector3 ((randomX/2)/100f,(randomY/2)/100f,-10f);
		mc.transform.position = cameraPos; 

		System.Random rnd = new System.Random();
		int rndg;

		for(float x = 0;x<randomX;x=x+128)
		{
			for(float y = 0;y<randomY;y=y+128)
			{	
				rndg = rnd.Next(1, 4);

				if(rndg == 1){
					Instantiate(gem1,new Vector3(x/100, y/100, 0),Quaternion.identity);
				}else if(rndg == 2){
					Instantiate(gem2,new Vector3(x/100, y/100, 0),Quaternion.identity);
				}else if(rndg == 3){
					Instantiate(gem3,new Vector3(x/100, y/100, 0),Quaternion.identity);
				}else{
					Instantiate(gem4,new Vector3(x/100, y/100, 0),Quaternion.identity);
				}


				GameObject newGem = GameObject.Find("gem" + rndg + "(Clone)");
				newGem.name=("Gem (" + x + ", " + y + ")");
				newGem.transform.parent = Map.transform;
				mops++;			
			}			
		}

		EdgeCollider2D edgeCol = Map.AddComponent<EdgeCollider2D> ();

		Vector2[] colliderpoints = new Vector2[5];
		colliderpoints[0] = new Vector2(-0.01f,-0.01f);
		colliderpoints[1] = new Vector2(-0.01f,(randomY+1)/100f);
		colliderpoints [2] = new Vector2 ((randomX+1)/100f, (randomY+1)/100f);
		colliderpoints [3] = new Vector2 ((randomX+1)/100f, -0.01f);
		colliderpoints [4] = new Vector2 (-0.01f,-0.01f);
		edgeCol.points =  colliderpoints;

		GameObject gc = GameObject.Find("GameController");		
		gameController levelScript = gc.GetComponent<gameController>();
		levelScript.toMop = mops-1;
		levelScript.maxX = maxX;
		levelScript.maxY = maxY;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
