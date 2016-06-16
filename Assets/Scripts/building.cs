using UnityEngine;
using System.Collections;

public class building : MonoBehaviour {

	public unit_spawner spawner;

	void OnDestroy() {
		if (spawner)
			spawner.buildingDestroyed ();
	}

}
