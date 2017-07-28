using UnityEngine;
using System.Collections;

public class Sun_Clock : MonoBehaviour {

	//Variables
	public float TimeScale = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float speed = (TimeScale / 360);
		float rotation = speed * Time.deltaTime;
		transform.Rotate (new Vector3 (rotation, 0, 0));
	}
}
