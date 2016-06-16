using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	public class manager_orcs : MonoBehaviour {

	public static manager_orcs instance { get; private set; }

	public entity_ex03 castle;

	public List<entity_ex03> black_list = new List<entity_ex03> ();

	public List<footman_ex01> hitlist;
	public List<footman_ex01> orcs_all;

	
	void Awake() {
		instance = this;
		hitlist = new List<footman_ex01> ();
		orcs_all = new List<footman_ex01> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		orcs_all.RemoveAll(isDead);
		black_list.RemoveAll(wereCrushed);
		int victim_count = black_list.Count;
		if (victim_count > 0) {
			foreach (footman_ex01 orc in orcs_all) {
				if (orc.target_entity == null) {
					orc.target_entity = black_list[Random.Range(0, victim_count)];
				}
			}
		}
		if (castle.hit_points < 71) {
			hitlist.RemoveAll(isDead);
			int orc_index = orcs_all.Count -1;
			foreach (footman_ex01 footman in hitlist) {
				if (orc_index < 0)
					break;
				if (footman.target_entity == castle) {
					orcs_all[orc_index].target_entity = footman.GetComponent<entity_ex03>();
					orc_index--;
				}
			}
		}
	}

	public void Add(footman_ex01 orc) {
		orcs_all.Add (orc);
	}

	public void AddToHitList(footman_ex01 footman) {
		hitlist.Add (footman);
	}
	
	public bool isDead(footman_ex01 footman) {
		return (footman == null);
	}

	public bool wereCrushed(entity_ex03 victim) {
		return (victim == null);
	}
}
