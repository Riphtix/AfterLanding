using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	private static UIManager instance;

	public GameObject pauseMenu;

	public static UIManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.Find("UIManager").GetComponent<UIManager>();
			}
			return instance;
		}

		set {
			instance = value;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void togglePauseMenu(bool toggle) {
		pauseMenu.SetActive(toggle);
	}
}
