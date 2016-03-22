using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO; 

public class levelGenerator : MonoBehaviour {

	public GameObject gameController, gem1, gem2, gem3, gem4, lava2;
	public int mops = 0;
	public int maxX, maxY;
	private string[] entries, read;
    private string fileName = "C:/Christopher/University Materials/4th Year/Honours Project/The-Knight-s-Escape/Assets/Text Levels/ktTestLevel.txt";
    //public FileInfo theSourceFile; 
    //= new FileInfo("Test.txt");
    //public string fileName = "Dave";
    private List<int> wallList = new List<int>();
    GameObject ctm;
    clickToMove ls;

	// Use this for initialization
	void Start () {


        GameObject Walls = new GameObject();
        Walls.name = "Walls";
        if (Load (fileName)) System.Console.WriteLine("Level Loaded\n");

			GameObject Map = new GameObject ();
			Map.name = ("Map Components");

			//System.Random random = new System.Random ();
			//int randomX = random.Next (2, 5);
			//int randomY = random.Next (2, 5);

			int randomX = System.Int32.Parse(entries[0]);
			int randomY = System.Int32.Parse(entries[1]);

			int startX = System.Int32.Parse(entries[2]);
			int startY = System.Int32.Parse(entries[3]);

			for(int i=4;i<entries.Length;i++){
				wallList.Add (System.Int32.Parse(entries[i]));
			}
			int width = randomX;
			int height = randomY;

			randomX = randomX * 128;
			randomY = randomY * 128;

			maxX = randomX;
			maxY = randomY;

			GameObject mc = GameObject.Find ("Main Camera");
			Vector3 cameraPos = new Vector3 ((randomX / 2) / 100f, (randomY / 2) / 100f, -10f);
			mc.transform.position = cameraPos; 

			System.Random rnd = new System.Random ();
			int rndg;

			for (float x = 0; x<randomX; x=x+128) {
				for (float y = 0; y<randomY; y=y+128) {	
					rndg = rnd.Next (1, 5);

					if (rndg == 1) {
						Instantiate (gem1, new Vector3 (x / 100, y / 100, 0), Quaternion.identity);
					} else if (rndg == 2) {
						Instantiate (gem2, new Vector3 (x / 100, y / 100, 0), Quaternion.identity);
					} else if (rndg == 3) {
						Instantiate (gem3, new Vector3 (x / 100, y / 100, 0), Quaternion.identity);
					} else {
						Instantiate (gem4, new Vector3 (x / 100, y / 100, 0), Quaternion.identity);
					}


					GameObject newGem = GameObject.Find ("gem" + rndg + "(Clone)");
					newGem.name = ("Gem (" + x + ", " + y + ")");
					newGem.transform.parent = Map.transform;
					mops++;			
				}			
			}

			EdgeCollider2D edgeCol = Map.AddComponent<EdgeCollider2D> ();

			Vector2[] colliderpoints = new Vector2[5];
			colliderpoints [0] = new Vector2 (-0.01f, -0.01f);
			colliderpoints [1] = new Vector2 (-0.01f, (randomY + 1) / 100f);
			colliderpoints [2] = new Vector2 ((randomX + 1) / 100f, (randomY + 1) / 100f);
			colliderpoints [3] = new Vector2 ((randomX + 1) / 100f, -0.01f);
			colliderpoints [4] = new Vector2 (-0.01f, -0.01f);
			edgeCol.points = colliderpoints;

			while (wallList.Count > 0) {

            float fx = wallList[0] % width;
                fx = (fx*128)/100;
				float fy = ((Mathf.Round(wallList[0] / width))*128)/100;

				Vector3 statonaryPos = new Vector3(fx, fy, 0f);

				Instantiate (lava2, (statonaryPos), Quaternion.identity);
				GameObject newMopped = GameObject.Find("lava2(Clone)");
				newMopped.name=("Lava2 " + statonaryPos);
				newMopped.transform.parent = Walls.transform;

            ctm = GameObject.Find("Player");
            ls = ctm.GetComponent<clickToMove>();
            ls.posList.Add(statonaryPos);

            wallList.Remove (wallList[0]);
				mops--;
			}
            
        GameObject gc = GameObject.Find("GameController");
        gameController levelScript = gc.GetComponent<gameController>();
        levelScript.toMop = mops - 1;
        levelScript.maxX = maxX;
        levelScript.maxY = maxY;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            //StreamReader thereader = theSourceFile.OpenText();
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
			{
				int hasRead = 0;
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					read = line.Split(',');
					if (hasRead == 0)
					{
						entries = read;
						hasRead = 1;
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (System.Exception e)
		{
			System.Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}

	// string text = System.IO.File.ReadAllText("ktgame.param.txt");
}
