using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	private static GameObject pauseMenu;

	public static GameObject PauseMenu {
		get {
			if(pauseMenu == null) {
				pauseMenu = GameObject.Find("Pause Menu");
				pauseMenu.SetActive(false);
			}
			return pauseMenu;
		}

		set {
			pauseMenu = value;
		}
	}

	// Use this for initialization
	void Start () {
		PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void togglePauseMenu(bool toggle) {
		PauseMenu.SetActive(toggle);
	}
}
