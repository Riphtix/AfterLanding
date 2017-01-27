using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private static Player instance;

	private static int maxHealth;
	private static int xpToNextLevel;
	private static int currentLevel;
	private static int currentHealth;
	private static int currentXP;
	private static int upgradePoints;

	public Sprite sprite;
	public float moveSpeed = .1f;
	public float rotationSpeed = 5f;
	public Vector3 moveVector { set; get; }
	public Vector3 rotVector { set; get; }
	public VirtualJoystick moveJoystick;
	public VirtualJoystick rotJoystick;
	
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
				CurrentHealth = maxHealth;
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

	public static int UpgradePoints {
		get {
			return upgradePoints;
		}

		set {
			upgradePoints = value;
		}
	}

	// Use this for initialization
	void Start () {
		if (CurrentLevel < 1) {
			ExperienceManager.Instance.levelUp();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentXP >= XpToNextLevel) {
			ExperienceManager.Instance.levelUp();
		}

		moveVector = poolInput();
		rotVector = rotInput();

		move();
		turn();

		GameObject.Find("HP").GetComponent<Text>().text = "HP: " + CurrentHealth + "/" + MaxHealth;
		GameObject.Find("XP").GetComponent<Text>().text = "XP: " + CurrentXP + "/" + XpToNextLevel;
		GameObject.Find("Level").GetComponent<Text>().text = "LVL: " + CurrentLevel;
	}

	private void move() {
		transform.position += moveVector * moveSpeed;
	}

	private void turn() {
		transform.rotation = new Quaternion(rotVector.x, rotVector.y, 0, 0);
	}

	private Vector3 poolInput() {
		Vector3 dir = Vector3.zero;

		dir.x = moveJoystick.horizontal();
		dir.y = moveJoystick.vertical();

		if(dir.magnitude > 1) {
			dir.Normalize();
		}

		return dir;
	}

	private Vector3 rotInput() {
		Vector3 dir = Vector3.zero;
		
		dir.x = rotJoystick.horizontal();
		dir.y = rotJoystick.vertical();
		dir.z = 0;

		return dir;
	}
}
