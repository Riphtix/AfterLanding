using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour {
	public GameObject health;
	public GameObject speed;
	public GameObject armor;
	public GameObject aim;
	public Sprite pointSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void increaseHealth() {
		Player.MaxHealth += 5;
		addPoint(health);
		Player.UpgradePoints -= 1;
	}

	public void addPoint(GameObject parent) {
		GameObject point = new GameObject();
		point.AddComponent<Image>();
		point.GetComponent<Image>().sprite = pointSprite;
		point.transform.SetParent(parent.transform.GetChild(1));
		point.transform.localPosition = new Vector3(0, 0, 0);
	}
}
