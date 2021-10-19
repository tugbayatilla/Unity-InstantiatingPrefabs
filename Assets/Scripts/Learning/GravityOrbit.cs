using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
	public Transform target;
	public float orbitHeightOffset = 0.5f;
	public float speed = 3.0f;

	// Update is called once per frame
	void Update()
	{
		Quaternion rotation = GetRotation();

		LookAtUsingRotation(rotation);
		
		Rotate();
	}

	private void Rotate()
	{
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	private void LookAtUsingRotation(Quaternion rotation)
	{
		Quaternion current = transform.localRotation;
		transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
	}

	private Quaternion GetRotation()
	{
		Vector3 offset = new Vector3(0, orbitHeightOffset, 0);
		Vector3 relativePos = (target.position + offset) - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		return rotation;
	}
}
