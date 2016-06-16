using UnityEngine;
using System.Collections;

public class entity_ex03 : MonoBehaviour {

	public string unit_name = "Anon";
	public int alliance = 0;
	public int hit_points = 100;
	public int attack = 0;
	public float attack_interval = 1.0f;
	public bool is_human_castle = false;
	public bool is_orc_castle = false;

	private int max_hp;
	private bool game_over;
	private float attack_timer;

	// Use this for initialization
	void Start () {
		max_hp = hit_points;
		game_over = false;
		attack_timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		attack_timer += Time.deltaTime;
		if (is_human_castle && !game_over && hit_points < 1) {
			game_over = true;
			Debug.Log ("The Orc Team wins.");
		}
		if (is_orc_castle && !game_over && hit_points < 1) {
			game_over = true;
			Debug.Log ("The Human Team wins.");
		}
		if (hit_points < 1) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		entity_ex03 enemy = coll.gameObject.GetComponent<entity_ex03>();
		footman_ex01 footman = GetComponent<footman_ex01>();
		orc_ex02 orc = GetComponent<orc_ex02>();
		if (footman && enemy && enemy.alliance != alliance)
			footman.target_entity = enemy;
		if (orc && enemy && enemy.alliance != alliance)
			orc.target_entity = enemy;
	}

	void OnCollisionStay2D(Collision2D coll) {
		entity_ex03 enemy = coll.gameObject.GetComponent<entity_ex03>();
		if (enemy && attack > 0 && enemy.alliance != alliance && attack_timer >= attack_interval) {
			attack_timer = 0.0f;
			enemy.hit_points -= attack;
			Debug.Log (enemy.unit_name + " [" + enemy.hit_points + "/" + enemy.max_hp + "] has been attacked.");
		}
	}
	
	void OnCollisionExit2D(Collision2D coll) {
	}
}
