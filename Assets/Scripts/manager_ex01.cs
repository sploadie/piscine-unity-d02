using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class manager_ex01 : MonoBehaviour {

	public static manager_ex01 instance { get; private set; }

	public List<footman_ex01> footmen_all { get; private set; }
	public List<footman_ex01> footmen_selected { get; private set; }
	
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
			foreach (footman_ex01 footman in footmen_all) {
				if (footman.collider2D == Physics2D.OverlapPoint(pos)) {
					// If footman touched
					if (!Input.GetKey("control"))
						footmen_selected.Clear();
					footmen_selected.Add(footman);
					break;
				}
			}
			// If no footman touched
			foreach (footman_ex01 footman in footmen_selected) {
				footman.newTarget(pos);
			}
		} else if (Input.GetMouseButtonDown (1)) {
			footmen_selected.Clear();
		}
	}

	public void Add(footman_ex01 footman) {
		footmen.Add(footman);
	}
}
