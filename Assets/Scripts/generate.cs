using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class generate : MonoBehaviour {

	public GameObject pair;
	public GameObject spikepair;
	public GameObject invboard;



	public GameObject metronome;
	private GameObject newmetro;
	public GameObject tmpparent;

	//private ArrayList map = new ArrayList();
	float[,] map = new float[,] {{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0},{1,0},{1,1},{1,0},{1,0}};//<duration, board/spike/space/invisible>
	float[,] map1 = new float[,] {{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0},{1,0},{1,2},{1,0},{1,0}};
	float[,] map2 = new float[,] {{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0},{1,0},{1,1},{1,2},{1,0}};
	float[,] map3 = new float[,] {{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1},{1,2},{1,0},{1,2},{1,1}};
	//private Coroutine coroutine;
	public Queue boards = new Queue ();
	private float[,] p0 = new float[,] {{1,0},{1,1},{1,0},{1,0}};
	private float[,] p1 = new float[,] {{1,0},{1,2},{1,0},{1,0}};
	private float[,] p2 = new float[,] {{1,2},{1,0},{1,0},{1,0}};
	private float[,] p3 = new float[,] {{1,0},{1,1},{1,2},{1,0}};
	private float[,] p4 = new float[,] {{1,2},{1,1},{1,2},{1,1}};
	private float[,] p5 = new float[,] {{4.0f/3,1},{4.0f/3,1},{4.0f/3,1}};

	// Use this for initialization
	void Start()
	{
		//InvokeRepeating("CreateObstacle", 1f, 1.5f);
		//MapGenerate (generatemap(10,2));
		if(LoadOnClick.mode == 0){
			if (LoadOnClick.pattern == 0) {
				MapGenerate (map);
			}
			if (LoadOnClick.pattern == 1) {
				MapGenerate (map1);
			}
			if (LoadOnClick.pattern == 2) {
				MapGenerate (map2);
			}
			if (LoadOnClick.pattern == 3) {
				MapGenerate (map3);
			}
		}
		Debug.Log (LoadOnClick.level);
		if (LoadOnClick.mode == 1) {
			MapGenerate (generatemap (10, LoadOnClick.level));
		}

		//MapGenerate (map);
		newmetro = Instantiate	 (metronome);
		tmpparent.GetComponent<rock> ().enabled = true;
	}
	void Update()
	{
		try{
			GameObject tmppair = (GameObject)boards.Peek ();
			//Debug.Log("count"+boards.Count);
			if(boards.Count==1){
				Debug.Log("finished");
				SceneManager.LoadScene(3);
			}
			//Debug.Log ("newpair"+tmppair.gameObject.transform.position);
			Vector2 screenPosition = Camera.main.WorldToScreenPoint(tmppair.transform.position);
			if (screenPosition.x < -Screen.width)
			{
				Destroy(tmppair.gameObject);
				//tmppair.gameObject.SetActive (false);
				boards.Dequeue ();
				Debug.Log ("deque");
			}
		}catch {
			
		}

	}

	public void die(){
		Destroy(newmetro);
		foreach (GameObject pair in boards) {
			Destroy (pair);

		}	
		boards.Clear ();

		if(LoadOnClick.mode == 0){
			if (LoadOnClick.pattern == 0) {
				MapGenerate (map);
			}
			if (LoadOnClick.pattern == 1) {
				MapGenerate (map1);
			}
			if (LoadOnClick.pattern == 2) {
				MapGenerate (map2);
			}
			if (LoadOnClick.pattern == 3) {
				MapGenerate (map3);
			}
		}
		Debug.Log (LoadOnClick.level);
		if (LoadOnClick.mode == 1) {
			MapGenerate (generatemap (10, LoadOnClick.level));
		}


		//MapGenerate (map);
		newmetro = Instantiate (metronome);

	}


	//return a list of different patterns according to distribution
	private ArrayList generatemap(int length, int level){
		ArrayList map = new ArrayList ();
		float[] distribution = generatedistribution (level);
		for (int i = 1; i < length; i++) {
			float sample = Random.Range (0.0f,1.0f);
			if (sample < distribution[0]) {
				map.Add (p0);
				//Debug.Log ("p0");
			} else if (sample >= distribution[0] && sample < distribution[1]) {
				map.Add (p1);
				//Debug.Log ("p1");
			} else if (sample >= distribution[1] && sample < distribution[2]) {
				map.Add (p2);
				//Debug.Log ("p2");
			} else if (sample >= distribution[2] && sample < distribution[3]) {
				map.Add (p3);
			//	Debug.Log ("p3");
			} else if (sample >= distribution[3] && sample < distribution[4]) {
				map.Add (p4);
				//Debug.Log ("p4");
			} else if (sample >= distribution[4] && sample < 1) {
				map.Add (p5);
				//Debug.Log ("p5");
			} 
		}

		return map;
	}


	/**
	generate map for tutorial
	*/
	private void MapGenerate(float[,] map){
		Vector2 iniposion = new Vector2 (0,0);
		float tmpx = 0;
		float diff = 15.2f / 4.0f;
		for(int i=0;i<map.Length/2;i++) {
						
			pair.transform.position = new Vector2 (iniposion.x+tmpx*diff, iniposion.y);
			spikepair.transform.position = new Vector2 (iniposion.x+tmpx*diff, iniposion.y);
			invboard.transform.position = new Vector2 (iniposion.x+tmpx*diff, iniposion.y);
			if (map [i, 1] == 0) {
				GameObject newpair = (GameObject)Instantiate (pair);
				newpair.transform.SetParent (tmpparent.transform);
				//Debug.Log ("parent set");
				boards.Enqueue (newpair);
			} else if (map [i, 1] == 1) {
				GameObject newpair = (GameObject)Instantiate (spikepair);
				newpair.transform.SetParent (tmpparent.transform);
				boards.Enqueue (newpair);
			} else if (map [i, 1] == 3) {
				GameObject newpair = (GameObject)Instantiate (invboard);
				newpair.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = false;
				newpair.transform.SetParent (tmpparent.transform);
				boards.Enqueue (newpair);
			}

			tmpx += map[i,0];
		}


	
	}
	//Inistiate map
	private void MapGenerate(ArrayList map){
		Vector2 iniposion = new Vector2 (0, 0);
		float tmpx = 0;
		float diff = 15.2f / 4.0f;
		foreach(float[,] pattern in map){
			for (int i = 0; i < pattern.Length / 2; i++) {
				pair.transform.position = new Vector2 (iniposion.x+tmpx*diff, iniposion.y);
				spikepair.transform.position = new Vector2 (iniposion.x+tmpx*diff, iniposion.y);
				if (pattern [i, 1] == 0) {
					GameObject newpair = (GameObject)Instantiate (pair);
					newpair.transform.SetParent (tmpparent.transform);
					//Debug.Log ("parent set");
					boards.Enqueue (newpair);
				} else if (pattern [i, 1] == 1) {
					GameObject newpair = (GameObject)Instantiate (spikepair);
					newpair.transform.SetParent (tmpparent.transform);
					boards.Enqueue (newpair);
				} 
				tmpx += pattern[i,0];
			}
		}
	
	
	}
	/**
	define distribution
	*/
	private float[] generatedistribution(int level){
		switch (level) {
		case 1:
			return new float[] {0.2f,0.5f,0.7f,0.8f,0.9f};
		case 2:
			return new float[] {0.2f,0.4f,0.7f,0.8f,0.9f};
		case 3:
			return new float[] {0.1f,0.3f,0.4f,0.6f,0.8f};
		default:
			return new float[] {0.2f,0.5f,0.7f,0.8f,0.9f};
		
		}


	}



//	private IEnumerator setmap(float[] map){
//		//yield return new WaitForSeconds (60 / bpm);
//		/*
//		foreach(float[] times in map){
//			
//			foreach (float time in times) {
//				
//
//				//Debug.Log (boards.Peek());
//				yield return new WaitForSeconds(time*(120/bpm));
//
//				GameObject newpair = (GameObject)Instantiate (pair);
//				boards.Enqueue (newpair);
//			}
//
//		}
//	    */
//		foreach (float time in map) {
//			yield return new WaitForSeconds(time*(60.0f/bpm));
//			GameObject newpair = (GameObject)Instantiate (pair);
//			boards.Enqueue (newpair);
//
//		}
//	}

}
