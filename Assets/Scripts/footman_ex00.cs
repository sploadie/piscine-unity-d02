using UnityEngine;
using System.Collections;

public class footman_ex00 : MonoBehaviour {

	public Vector3 target;
	public float direction;
	public Vector3 direction_vector;
	public float speed = 3.0f;
	public float max_distance = 0.0f;

	private Animator animator;
	private float footman_z;
	private bool targeting;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		footman_z = this.transform.position.z;
		targeting = false;
		animator.speed = 0;
		animator.SetFloat ("Direction", -1);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;
		if (Input.GetMouseButtonDown (0)) {
			GetComponent<unit_sounds>().Play("Acknowledge");
			pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = footman_z;
			target = pos;
			direction_vector = (pos - this.transform.position).normalized;
			direction = Vector3.Dot (Vector3.up, direction_vector);
			animator.SetFloat ("Direction", direction);
			if (pos.x < this.transform.position.x) {
				this.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				this.transform.localScale = new Vector3 (1, 1, 1);
			}
			targeting = true;
			animator.speed = 1;
		}
		if (targeting) {
			pos = this.transform.position;
			float old_distance = (this.transform.position - target).magnitude;
			Vector3 displacement = direction_vector * speed * Time.deltaTime;
//			Debug.Log ("Distance: "+old_distance+" Displacement: "+displacement.magnitude);
			if (old_distance > displacement.magnitude) {
				this.transform.position += displacement;
			}
			if (old_distance <= displacement.magnitude) {
				this.transform.position = target - (direction_vector * (max_distance / 2));
			}
			if (old_distance <= displacement.magnitude + max_distance) {
				targeting = false;
				animator.speed = 0;
				animator.Play("Walking",0, 0);
			}
		}
	}
}
