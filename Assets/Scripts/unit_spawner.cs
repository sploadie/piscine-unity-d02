using UnityEngine;
using System.Collections;

public class unit_spawner : MonoBehaviour {

	public GameObject unit;
	public float base_spawn_interval = 10.0f;

	private float timer;
	private float spawn_interval;
	
	// Use this for initialization
	void Start () {
		spawn_interval = base_spawn_interval;
		timer = base_spawn_interval;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= spawn_interval) {
			timer = 0.0f;
			GameObject.Instantiate(unit, transform.position, transform.rotation);
		}
	}
}
