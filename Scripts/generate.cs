using UnityEngine;
using System.Collections;


public class generate : MonoBehaviour {

	public GameObject pair;
	//private ArrayList rocks;
	private float[] map;
	private Coroutine coroutine;
	public Queue boards = new Queue ();
	// Use this for initialization
	void Start()
	{
		//InvokeRepeating("CreateObstacle", 1f, 1.5f);
		//rocks = new ArrayList();
		map = new float[] {1,0.5f,0.5f,1f,1,1f,1f,1f,1,1f,1f,1f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f};

		coroutine = StartCoroutine(setmap (map));
	}
	void Update()
	{
		try{
			GameObject tmppair = (GameObject)boards.Peek ();

			Debug.Log ("newpair"+tmppair.gameObject.transform.position);
			Vector2 screenPosition = Camera.main.WorldToScreenPoint(tmppair.transform.position);
			if (screenPosition.x < -Screen.width)
			{
				tmppair.gameObject.SetActive (false);
				boards.Dequeue ();
				Debug.Log ("deque");
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
		

		coroutine = StartCoroutine (setmap(map));
	
	}

	private IEnumerator setmap(float[] map){
		foreach(float times in map){
			GameObject newpair = (GameObject)Instantiate (pair);
			boards.Enqueue (newpair);
			//Debug.Log (boards.Peek());
			yield return new WaitForSeconds(times);
		}
	}

}
