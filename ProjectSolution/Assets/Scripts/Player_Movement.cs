using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	// Variables
	public float moveSpeed;
	public float moveSmooth;
	public float jumpHeight;
	private float jumpSpeed;
	private Animator anim;
	private float moveY;
	private float moveX;
	private bool combat;
	private bool steath;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		jumpSpeed = Mathf.Sqrt (2 * -Physics.gravity.y * jumpHeight);
	}

	// Update is called once per frame
	void Update () {
		float toMoveY = Input.GetAxisRaw ("Move Vertical");
		float toMoveX = Input.GetAxisRaw ("Move Horizontal");

		if ((Input.GetButton("Sprint")) && (moveY > 0f) && (hasSP()) && (!isJumping()) && (!isFalling())) {
			toMoveY = 2f;
			toMoveX = 0f;
		}

		if ((toMoveY != 0) && (toMoveX != 0)) {
			toMoveX *= 0.7f;
			toMoveY *= 0.7f;
		}

		moveY = Mathf.Lerp (moveY, toMoveY, Time.deltaTime * moveSmooth);
		moveX = Mathf.Lerp (moveX, toMoveX, Time.deltaTime * moveSmooth);

		anim.SetFloat ("SpeedY", moveY);
		anim.SetFloat ("SpeedX", moveX);

		Debug.DrawRay (transform.position - Vector3.down, Vector3.down * 2f);
		if (Physics.Raycast(transform.position - Vector3.down, Vector3.down, 2f)) {
			anim.SetBool ("InAir", false);
		}
		else {
			anim.SetBool ("InAir", true);
		}

		if ((Input.GetButtonDown("Jump")) && (!anim.GetBool("InAir")) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MoveInGround"))) {
			anim.SetBool ("Jump", true);
		}

		if ((Input.GetButtonDown("Attack")) && (!anim.GetBool("InAir")) && (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MoveInGround")) && (!Input.GetButton("Sprint"))) {
			anim.SetTrigger ("Swing");
		}
	}

	void OnAnimatorMove () {
		GetComponent<Rigidbody>().velocity =	(transform.forward * anim.GetFloat("SpeedY") * moveSpeed) +
								(transform.right * anim.GetFloat("SpeedX") * moveSpeed) +
								(transform.up * GetComponent<Rigidbody>().velocity.y + transform.up * (jumpTrigger() ? 1 : 0) * jumpSpeed);
	}

	bool jumpTrigger() {
		if ((anim.GetBool ("Jump")) && ((anim.GetAnimatorTransitionInfo (0).IsName ("Base Layer.JumpStance -> Base Layer.Jump")) || (anim.GetAnimatorTransitionInfo (0).IsName ("Base Layer.MoveInGround -> Base Layer.Jump")))) {
			anim.SetBool ("Jump", false);
			return true;
		}
		else {
			return false;
		}
	}

	bool hasSP() {
		return (GetComponent<Player_Status> ().SP > 0);
	}

	bool isJumping() {
		return (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump") || anim.GetCurrentAnimatorStateInfo (0).IsName ("JumpStance"));
	}

	bool isFalling() {
		return (anim.GetCurrentAnimatorStateInfo (0).IsName ("Fall"));
	}
}