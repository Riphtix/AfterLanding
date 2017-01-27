using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour {
	private static ExperienceManager instance;

	public GameObject health;
	public GameObject speed;
	public GameObject armor;
	public GameObject aim;
	public GameObject levelUpMenu;
	public Sprite pointSprite;

	public static ExperienceManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.Find("XP").GetComponent<ExperienceManager>();
			}
			return instance;
		}

		set {
			instance = value;
		}
	}


	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (levelUpMenu.activeSelf) {
			GameObject.Find("Upgrade Points").GetComponent<Text>().text = "Upgrade Points: " + Player.UpgradePoints;
			health.transform.GetChild(2).GetComponent<Text>().text = health.transform.GetChild(1).childCount.ToString();
			speed.transform.GetChild(2).GetComponent<Text>().text = speed.transform.GetChild(1).childCount.ToString();
			armor.transform.GetChild(2).GetComponent<Text>().text = armor.transform.GetChild(1).childCount.ToString();
			aim.transform.GetChild(2).GetComponent<Text>().text = aim.transform.GetChild(1).childCount.ToString();
		}
	}

	public void levelUp() {
		Player.CurrentXP -= Player.XpToNextLevel;
		Player.CurrentLevel += 1;
		setXpToNextLevel(Player.CurrentLevel);

		if (Player.CurrentLevel != 1) {
			toggleLevelUpMenu(true);
			awardPoints(Player.CurrentLevel);
		}
	}

	public void increaseHealth() {
		if (Player.UpgradePoints > 0) {
			Player.MaxHealth += 5;
			addPoint(health);
			if (health.transform.GetChild(1).childCount >= 20) {
				health.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void addPoint(GameObject parent) {
			GameObject point = new GameObject();
			point.AddComponent<Image>();
			point.GetComponent<Image>().sprite = pointSprite;
			point.transform.SetParent(parent.transform.GetChild(1));
			point.transform.localPosition = new Vector3(0, 0, 0);
			Player.UpgradePoints -= 1;
	}

	public void setXpToNextLevel(int currentLevel) {
		Player.XpToNextLevel = (int) ((35 * currentLevel) + (0.2 * (35 * currentLevel)));
	}

	public void awardPoints(int currentLevel) {
		Player.UpgradePoints += (int) (currentLevel * 0.5);
	}

	public void addExperience() {
		Player.CurrentXP += Player.XpToNextLevel;
	}

	public void toggleLevelUpMenu(bool toggle) {
		levelUpMenu.SetActive(toggle);
	}
}
