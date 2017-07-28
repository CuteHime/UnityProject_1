using UnityEngine;
using System.Collections;

public class Player_Menu : MonoBehaviour {
	
	private bool paused;
	public GameObject pauseMenu;
	private GameObject menu;

	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Status Menu")) {
			if (!paused) {
				menu = Instantiate (pauseMenu, Vector3.zero, Quaternion.identity) as GameObject;
				paused = true;
				Debug.Log (menu);
			}
			else {
				Destroy(menu);
				paused = false;
			}
		}
	}
}
