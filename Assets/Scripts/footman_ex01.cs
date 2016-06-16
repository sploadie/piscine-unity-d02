﻿using UnityEngine;
using System.Collections;

public class footman_ex01 : MonoBehaviour {

	public bool footman = false;
	public bool dead;
	
	public float speed = 3.0f;
	public float max_distance = 0.0f;
	public entity_ex03 target_entity;
	
	private Animator animator;
	private float footman_z;
	
	private bool new_target;
	private bool targeting;
	private bool targeting_entity;
	private Vector3 target;

	// Use this for initialization
	void Start () {
		dead = false;
		animator = GetComponent<Animator>();
		footman_z = this.transform.position.z;
		new_target = false;
		targeting = false;
		target_entity = null;
		targeting_entity = false;
		animator.SetFloat ("Direction", -1);
		// Add to manager
		if (footman)
			manager_ex01.instance.Add (this);
	}

	// Update is called once per frame
	void Update () {
		if (dead)
			return;
		if (new_target) {
			new_target = false;
			target_entity = null;
			targeting_entity = false;
			GetComponent<unit_sounds>().Play("Acknowledge");
			animator.Play("Walking", 0, 0);
			animator.SetTrigger ("Walk");
			animator.speed = 1;
			targeting = true;
		}
		if (target_entity) {
//			animator.Play("Walking", 0, 0);
			animator.SetTrigger ("Walk");
			animator.speed = 1;
			targeting = true;
			targeting_entity = true;
			target = target_entity.transform.position;
			target.z = footman_z;
		}
		if (targeting_entity && target_entity == null) {
			targeting_entity = false;
			targeting = false;
			animator.speed = 0;
			animator.Play("Walking", 0, 0);
		}
		if (targeting) {
			Vector3 direction_vector = (target - this.transform.position).normalized;
			float direction = Vector3.Dot (Vector3.up, direction_vector);
			animator.SetFloat ("Direction", direction);
			if (target.x < this.transform.position.x) {
				this.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				this.transform.localScale = new Vector3 (1, 1, 1);
			}
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
				animator.Play("Walking", 0, 0);
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
