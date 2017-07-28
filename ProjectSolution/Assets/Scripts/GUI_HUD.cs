using UnityEngine;
using System.Collections;

public class GUI_HUD : MonoBehaviour {

	public GameObject Player;
	public GUITexture HPBar;
	public GUITexture MPBar;
	public GUITexture SPBar;
	private float HPOriWidth;
	private float MPOriWidth;
	private float SPOriWidth;

	// Use this for initialization
	void Start () {
		HPOriWidth = HPBar.pixelInset.width;
		MPOriWidth = MPBar.pixelInset.width;
		SPOriWidth = SPBar.pixelInset.width;
	}
	
	// Update is called once per frame
	void OnGUI () {
		Player_Status PlayerStatus = Player.GetComponent<Player_Status>();
		float HPPercent = (1f * PlayerStatus.HP / PlayerStatus.maxHP);
		HPBar.pixelInset = new Rect(HPBar.pixelInset.x, HPBar.pixelInset.y, HPOriWidth * HPPercent, HPBar.pixelInset.height);
		float MPPercent = (1f * PlayerStatus.MP / PlayerStatus.maxMP);
		MPBar.pixelInset = new Rect(MPBar.pixelInset.x, MPBar.pixelInset.y, MPOriWidth * MPPercent, MPBar.pixelInset.height);
		float SPPercent = (1f * PlayerStatus.SP / PlayerStatus.maxSP);
		SPBar.pixelInset = new Rect (SPBar.pixelInset.x, SPBar.pixelInset.y, SPOriWidth * SPPercent, SPBar.pixelInset.height);
	}
}
