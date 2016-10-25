using UnityEngine;
using System.Collections;


public class generate : MonoBehaviour {

	public GameObject pair;
	//private ArrayList rocks;
	private ArrayList map = new ArrayList();
	private Coroutine coroutine;
	public Queue boards = new Queue ();
	private float[] p0 = new float[] {1,1};
	private float[] p1 = new float[] { 2 };
	private float[] p2 = new float[] {0.5f,0.5f,0.5f,0.5f};
	private float[] p3 = new float[] {1,0.5f,0.5f};
	private float[] p4 = new float[] {0.5f,0.5f,1f};
	private float[] p5 = new float[] {0.5f,1,0.5f};

	// Use this for initialization
	void Start()
	{
		//InvokeRepeating("CreateObstacle", 1f, 1.5f);
		//rocks = new ArrayList();
		//map = new float[] {1,0.5f,0.5f,1f,1,1f,1f,1f,1,1f,1f,1f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f};

		coroutine = StartCoroutine(setmap (generatemap(10,1)));
	}
	void Update()
	{
		try{
			GameObject tmppair = (GameObject)boards.Peek ();

			//Debug.Log ("newpair"+tmppair.gameObject.transform.position);
			Vector2 screenPosition = Camera.main.WorldToScreenPoint(tmppair.transform.position);
			if (screenPosition.x < -Screen.width)
			{
				tmppair.gameObject.SetActive (false);
				boards.Dequeue ();
				//Debug.Log ("deque");
			}
		}catch {
			
		}

	}

	public void die(){
		StopCoroutine (coroutine);
		foreach (GameObject pair in boards) {
			pair.SetActive (false);
		}	
		boards.Clear ();
		restartCoroutine ();
	}

	public void restartCoroutine(){
		map.Clear ();
		generatemap (10, 2);
		coroutine = StartCoroutine (setmap(map));
	
	}

	private ArrayList generatemap(int length, int level){
		
		float[] distribution = generatedistribution (level);
		for (int i = 1; i < length; i++) {
			float sample = Random.Range (0.0f,1.0f);
			if (sample < distribution[0]) {
				map.Add (p0);
				Debug.Log ("p0");
			} else if (sample >= distribution[0] && sample < distribution[1]) {
				map.Add (p1);
				Debug.Log ("p1");
			} else if (sample >= distribution[1] && sample < distribution[2]) {
				map.Add (p2);
				Debug.Log ("p2");
			} else if (sample >= distribution[2] && sample < distribution[3]) {
				map.Add (p3);
				Debug.Log ("p3");
			} else if (sample >= distribution[3] && sample < distribution[4]) {
				map.Add (p4);
				Debug.Log ("p4");
			} else if (sample >= distribution[4] && sample < 1) {
				map.Add (p5);
				Debug.Log ("p5");
			} 
		}

		return map;
	}

	private float[] generatedistribution(int level){
		switch (level) {
		case 1:
			return new float[] {0.2f,0.5f,0.7f,0.8f,0.9f };
		case 2:
			return new float[] {0.2f,0.4f,0.7f,0.8f,0.9f};
		case 3:
			return new float[] { 0.1f, 0.3f, 0.4f, 0.6f, 0.8f};
		default:
			return new float[] {0.2f,0.5f,0.7f,0.8f,0.9f };
		
		}


	}

	private IEnumerator setmap(ArrayList map){
		foreach(float[] times in map){
			
			foreach (float time in times) {
				
				GameObject newpair = (GameObject)Instantiate (pair);
				boards.Enqueue (newpair);
				//Debug.Log (boards.Peek());
				yield return new WaitForSeconds(time);
			}

		}
	}

}
