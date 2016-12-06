using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public static int level = 0;
	//public int passlevel = 0;
	public static int pattern = 0;
	public static int mode = 0;


	public void NextLevelButton(int index)
	{
		
		SceneManager.LoadScene(index);
	}
	public void passvalue(int passlevel){
		level = passlevel;
	}
	public void choosepattern(int passpattern){
		pattern = passpattern;
	}

	public void choosemode(int passmode){
		mode = passmode;
	}


}