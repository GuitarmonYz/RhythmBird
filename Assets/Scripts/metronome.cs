using UnityEngine;
using System.Collections;

public class metronome : MonoBehaviour {

	public double bpm = 90.0f;

	int col = 0;
	int[] setting = { 1, 1, 1, 1 };
	double nextTick = 0.0f;
	//double sampleRate = 0.0f;
	bool ticked = false;
	//AudioSource mass;
	//AudioSource clear;
	AudioSource[] sourcearray;
	// Use this for initialization
	void Start () {
		sourcearray = GetComponents<AudioSource>();
		double startTick = AudioSettings.dspTime;
		//sampleRate = AudioSettings.outputSampleRate;
		nextTick = startTick + (60.0 / bpm);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if ( !ticked && nextTick >= AudioSettings.dspTime ) {
			ticked = true;
			BroadcastMessage( "OnTick" );
		}

	}
	void OnTick() {
		//Debug.Log( "Tick" );

		switch (col % 4) {
		case 0:
			sourcearray [0].Play();
			break;
		case 1:
			if (setting [1]==1) {
				sourcearray [1].Play ();			
			}
			break;
		case 2:
			if (setting [2]==1) {
				sourcearray [1].Play ();
			}
			break;
		case 3:
			if (setting [3]==1) {
				sourcearray [1].Play ();
			}
			break;
		}

		col++;
		// GetComponent<AudioSource>().Play();
	}

	void FixedUpdate() {
		double timePerTick = 60.0f / bpm;
		double dspTime = AudioSettings.dspTime;

		while (dspTime >= nextTick) {
			ticked = false;
			nextTick += timePerTick;
		}
	}


}
