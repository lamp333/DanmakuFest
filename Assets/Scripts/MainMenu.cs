using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// loads input level from main menu
	public void LoadScene(int level)
	{
		SceneManager.LoadScene (level);
	}
}
