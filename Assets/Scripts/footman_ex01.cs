using UnityEngine;
using System.Collections;

public class footman_ex01 : MonoBehaviour {

	public float speed = 3.0f;
	public float max_distance = 0.0f;

	private Animator animator;
	private float footman_z;
	
	private bool new_target;
	private bool targeting;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		footman_z = this.transform.position.z;
		new_target = false;
		targeting = false;
		animator.SetFloat ("Direction", -1);
		// Add to manager
		manager_ex01.instance.Add (this);
	}

	// Update is called once per frame
	void Update () {
		if (new_target) {
			new_target = false;
			GetComponent<unit_sounds>().Play("Acknowledge");
			animator.SetTrigger ("Walk");
			if (target.x < this.transform.position.x) {
				this.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				this.transform.localScale = new Vector3 (1, 1, 1);
			}
			targeting = true;
			animator.speed = 1;
		}
		if (targeting) {
			Vector3 direction_vector = (target - this.transform.position).normalized;
			float direction = Vector3.Dot (Vector3.up, direction_vector);
			animator.SetFloat ("Direction", direction);
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
//		this.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

	public void newTarget(Vector3 point) {
		point.z = footman_z;
		target = point;
		new_target = true;
	}
}
