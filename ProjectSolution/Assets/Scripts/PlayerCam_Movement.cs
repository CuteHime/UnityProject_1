using UnityEngine;
using System.Collections;

public class PlayerCam_Movement : MonoBehaviour {

	// Variables
	public float sensitivity;
	public Transform camera;
	public Transform point;
	public float minRotation;
	public float maxRotation;
	private float yRot;
	
	// Use this for initialization
	void Start () {
		camera.localPosition = new Vector3 (0.2f, 0f, -1f);
	}
	
	// Update is called once per frame
	void Update () {
		yRot = Input.GetAxis ("Camera Y-Axis") * sensitivity;
		if ((convertAngle(camera.eulerAngles.x) < convertAngle(minRotation) && yRot > 0) || (convertAngle(camera.eulerAngles.x) > convertAngle(maxRotation) && yRot < 0)) {
			yRot = 0;
		}
		point.Rotate (-Vector3.right, yRot);
		transform.Rotate (Vector3.up, Input.GetAxis ("Camera X-Axis") * sensitivity);

		float zRunCamPos;
		if ((GetComponent<Animator> ().GetFloat("SpeedY") > 1.1) && (Input.GetButton("Sprint"))) {
			zRunCamPos = -1.5f;
		}
		else {
			zRunCamPos = -1f;
		}
		camera.localPosition = Vector3.Lerp (camera.localPosition, new Vector3 (0.2f, 0f, zRunCamPos), Time.deltaTime * 1f);
	}
	
	float convertAngle(float angle) {
		float tmp = angle + 180f;
		if (tmp > 360f) tmp -= 360f;
		return tmp;
	}
}
