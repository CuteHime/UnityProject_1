using UnityEngine;
using System.Collections;

public class Player_Status : MonoBehaviour {

	// Variables
	public int lvl;
	public enum Job {Ahli_Pedang, Pemanah, Penyihir, Prajurit, Dukun, Pencuri};
	public Job job;
	public int EXP;
	public int nextLvl;
	public int maxHP;
	public int HP;
	public int maxMP;
	public int MP;
	public int maxSP;
	public int SP;
	public int totalWeight;
	public int weight;
	public int STR;			// affect weight and equipment req			
	public int VIT;			// affect HP and resistance
	public int AGI;			// affect SP and run speed
	public int INT;			// affect MP and magic req
	public int FOC;			// affect mag/skill atk and crit rate
	public int skillPointsPerLvl;
	public int skillPoints;
	public float HPDamage = 0;
	public float MPDamage = 0;
	public float SPDamage = 0;


	// Use this for initialization
	void Start () {
		lvl = 0;
		EXP = 2000;
		nextLvl = 0;
		job = jobAhli_PedangStartPoints();
		EXPCalculation (); 
	}
	
	// Update is called once per frame
	void Update () {

		// list of SP damage here
		if (isRunning()) {
			SPDamage += (20 - (AGI * 0.015f)) * Time.deltaTime;
		}
		else {
			SPDamage -= maxSP * 0.06f * Time.deltaTime;
		}
		if ((SPDamage >= 1) || (SPDamage <= -1)) {
			SP = UpdateBar(SP, maxSP, Mathf.FloorToInt(SPDamage));
			SPDamage = 0;
		}

		// list of MP damage here

		// list of HP damage here

	}

	string printJob () {
		if (job == Job.Ahli_Pedang) return "Ahli Pedang";
		if (job == Job.Pemanah) return "Pemanah";
		if (job == Job.Penyihir) return "Penyihir";
		if (job == Job.Prajurit) return "Prajurit";
		if (job == Job.Dukun) return "Dukun";
		if (job == Job.Pencuri) return "Pencuri";
		else return "error";
	}

	Job jobAhli_PedangStartPoints() {
		STR = 13;
		VIT = 8;
		AGI = 12;
		INT = 7;
		FOC = 10;
		return Job.Ahli_Pedang;
	}

	Job jobPemanahStartPoints() {
		STR = 9;
		VIT = 9;
		AGI = 10;
		INT = 8;
		FOC = 14;
		return Job.Pemanah;
	}

	Job jobPenyihirStartPoints() {
		STR = 10;
		VIT = 8;
		AGI = 6;
		INT = 14;
		FOC = 12;
		return Job.Penyihir;
	}

	Job jobPrajuritStartPoints() {
		STR = 15;
		VIT = 13;
		AGI = 9;
		INT = 6;
		FOC = 7;
		return Job.Ahli_Pedang;
	}

	Job jobDukunStartPoints() {
		STR = 5;
		VIT = 7;
		AGI = 9;
		INT = 16;
		FOC = 13;
		return Job.Dukun;
	}

	Job jobPencuriStartPoints() {
		STR = 7;
		VIT = 6;
		AGI = 15;
		INT = 12;
		FOC = 10;
		return Job.Pencuri;
	}

	bool isLvlUp () {
		return (EXP > nextLvl);
	}

	void EXPCalculation () {
		while (isLvlUp()) {
			lvl += 1;
			EXP -= nextLvl;
			if (nextLvl == 0) nextLvl = 700;
			else nextLvl *= 2;
			skillPoints += skillPointsPerLvl;
			maxHP = 100 + (10 * VIT) + (2 * lvl);
			HP = maxHP;
			maxMP = 100 + (10 * INT) + (2 * lvl);
			MP = maxMP;
			maxSP = 100 + (10 * AGI) + (2 * lvl);
			SP = maxSP;
			totalWeight = 200 + (5 * STR);
		}
	}

	bool isRunning() {
		return (Input.GetButton("Sprint") && GetComponent<Animator>().GetFloat("SpeedY") > 0 && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MoveInGround"));
	}

	int UpdateBar(int bar, int maxBar, int damage) {
		if ((bar >= 0) || (bar <= maxBar)) {
			bar -= damage;
		}

		if (bar > maxBar) {
			bar = maxBar;
		}
		else if (bar < 0) {
			bar = 0;
		}
		return bar;
	}
}
