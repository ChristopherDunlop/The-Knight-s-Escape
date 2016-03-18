using UnityEngine;
using System.Collections;

public class basicGenerator : MonoBehaviour {

	public GameObject center,topBar,sideBar,gameController;
	public int mops = 0;

	// Use this for initialization
	void Start () {
		GameObject Map = new GameObject();
		Map.name=("Map Components");

		for(float x = 0;x<1024;x=x+128)
		{
			for(float y = 0;y<768;y=y+96)
			{
				Instantiate(center,new Vector3(x/100, y/100, 0),Quaternion.identity);
				GameObject newCenter = GameObject.Find("centerColl(Clone)");
				newCenter.name=("Center (" + x + ", " + y + ")");
				newCenter.transform.parent = Map.transform;
				mops++;			
			}			
		}
		float pos1 = -97;
		float pos2 = 769;
		float pos3 = 1025;

		Instantiate(topBar,new Vector3(0, pos1/100, 0),Quaternion.identity);
		Instantiate(topBar,new Vector3(0, pos2/100, 0),Quaternion.identity);
		Instantiate(sideBar,new Vector3(pos1/100, 0, 0),Quaternion.identity);
		Instantiate(sideBar,new Vector3(pos3/100, 0, 0),Quaternion.identity);

		GameObject gc = GameObject.Find("GameController");		
		gameController levelScript = gc.GetComponent<gameController>();
		levelScript.toMop = mops-1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
