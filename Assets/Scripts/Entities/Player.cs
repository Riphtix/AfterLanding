using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private static Player instance;

	private static int maxHealth;
	private static int xpToNextLevel;
	private static int currentLevel;
	private static int currentHealth;
	private static int currentXP;

	public Sprite sprite;
	
	public static Player Instance {
		get {
			if(instance == null) {
				instance = GameObject.Find("Player").GetComponent<Player>();
			}
			return instance;
		}

		set {
			instance = value;
		}
	}

	public static int MaxHealth {
		get {
			if(maxHealth == 0) {
				maxHealth = 100;
			}
			return maxHealth;
		}

		set {
			maxHealth = value;
		}
	}

	public static int XpToNextLevel {
		get {
			return xpToNextLevel;
		}

		set {
			xpToNextLevel = value;
		}
	}

	public static int CurrentLevel {
		get {
			if(currentLevel == 0) {
				currentLevel = 1;
			}
			return currentLevel;
		}

		set {
			currentLevel = value;
		}
	}

	public static int CurrentHealth {
		get {
			return currentHealth;
		}

		set {
			currentHealth = value;
		}
	}

	public static int CurrentXP {
		get {
			return currentXP;
		}

		set {
			currentXP = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
