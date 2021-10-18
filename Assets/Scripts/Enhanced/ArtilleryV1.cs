using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enhanced
{
	public class ArtilleryV1 : MonoBehaviour
	{
		private const string TurretPivotName = "TurretPivot";
		private const string LauncherName = "Launcher";
		private float turretCurrentAngle = 0.0f;
		private Transform turretPivot;
		private Transform launcher;


		public ProjectileV1 projectilePrefab;
		
		public float turretMaxAngle = 80.0f;
		public float turretMinAngle = 10.0f;
		public float baseSpeed = 50.0f;
		public float turretSpeed = 20.0f;//todo: apply range

		void Start()
		{
			SetChildItems();

			ValidateChildItems();
		}

		void Update()
		{
			RotateHorizontal();

			RotateVertical();

			FireProjectile();
		}

		private void RotateHorizontal()
		{
			float rotation = Input.GetAxis("Horizontal") * baseSpeed;
			rotation *= Time.deltaTime;

			transform.Rotate(0, rotation, 0);
		}

		private void RotateVertical()
		{
			float translation = Input.GetAxis("Vertical") * turretSpeed;
			translation *= Time.deltaTime;

			turretCurrentAngle = Mathf.Clamp(turretCurrentAngle + translation, turretMinAngle, turretMaxAngle);
			turretPivot.localRotation = Quaternion.Euler(turretCurrentAngle, 0, 0);
		}

		private void FireProjectile()
		{
			if (!projectilePrefab) { return; }

			if (Input.GetButtonDown("Fire1"))
			{
				var projectile = Instantiate(projectilePrefab.gameObject, launcher.position, launcher.rotation);
				var rb = projectile.GetComponent<Rigidbody>();
				rb.velocity = launcher.forward * projectilePrefab.projectileInfo.speed;
				rb.mass = projectilePrefab.projectileInfo.mass;
			}
		}

		private void SetChildItems()
		{
			Transform[] children = transform.GetComponentsInChildren<Transform>();
			turretPivot = children.FirstOrDefault(p=>p.name == TurretPivotName);
			launcher = children.FirstOrDefault(p=>p.name == LauncherName);
		}

		private void ValidateChildItems()
		{
			if (turretPivot == null)
			{
				Debug.LogException(new Exception($"{nameof(turretPivot)} must be set."));
			}

			if (launcher == null)
			{
				Debug.LogException(new Exception($"{nameof(launcher)} must be set."));
			}
			
		}
		
	}
}