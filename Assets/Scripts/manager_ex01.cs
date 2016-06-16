using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class manager_ex01 : MonoBehaviour {

	public static manager_ex01 instance { get; private set; }

	public List<footman_ex01> footmen_all;
	public List<footman_ex01> footmen_selected;
	
	void Awake() {
		instance = this;
		footmen_all = new List<footman_ex01> ();
		footmen_selected = new List<footman_ex01> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			bool footman_clicked = false;
			footmen_all.RemoveAll(isDead);
			foreach (footman_ex01 footman in footmen_all) {
				if (footman.GetComponent<Collider2D>() == Physics2D.OverlapPoint(pos)) {
					// If footman touched
					if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
						footmen_selected.Clear();
					footmen_selected.Add(footman);
					footman.GetComponent<unit_sounds>().Play("Selected");
					footman_clicked = true;
					break;
				}
			}
			if (!footman_clicked) {
				// If no footman touched
				footmen_selected.RemoveAll(isDead);
				foreach (footman_ex01 footman in footmen_selected) {
					footman.newTarget(pos);
				}
			}
		} else if (Input.GetMouseButtonDown (1)) {
			footmen_selected.Clear();
		}
	}

	public void Add(footman_ex01 footman) {
		footmen_all.Add(footman);
	}

	public bool isDead(footman_ex01 footman) {
		return (footman == null);
	}
}
