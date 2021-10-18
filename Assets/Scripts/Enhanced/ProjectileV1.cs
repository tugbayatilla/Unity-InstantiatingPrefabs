using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enhanced
{
	public class ProjectileV1 : MonoBehaviour
	{
		public AudioSource audioSource;
		[SerializeField] private AudioClip explosionSound;
		public ParticleSystem explosion;

		//todo: make scriptableobject
		public ProjectileInfo projectileInfo;

		private bool isExploded = false;

		// Use this for initialization
		void Start()
		{
			//if (!audioSource)
			//{
			//	Debug.LogException(new Exception($"{nameof(audioSource)} must be set."));
			//}
			if (!projectileInfo)
			{
				Debug.LogException(new Exception($"{nameof(projectileInfo)} must be set."));
			}


			//destroy projectile after certain of time;
			Destroy(gameObject, projectileInfo.blastAfterSec);
		}



		// Update is called once per frame
		void Update()
		{
			if (isExploded)
			{
				Explosion();
				Destroy(gameObject);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			isExploded = true;
			
		}

		private void OnDestroy()
		{
			if (!isExploded) { 
				Explosion();
			}
		}

		private void Explosion()
		{
			if (explosion)
			{
				Instantiate(explosion, transform.position, transform.rotation);

				ExplosionEffect();

				if (explosionSound && audioSource)
				{
					audioSource.PlayOneShot(explosionSound);
				}

			}
		}

		private void ExplosionEffect()
		{
			// Explosion applies force to all nearby objects as soon as it is instantiated.
			var affectedColliders = Physics.OverlapSphere(transform.position, projectileInfo.blastRadius);

			// find all colliders overlapping the explosion radius
			foreach (var col in affectedColliders)
			{
				// apply a force to each of those colliders that has a rigidbody
				var rb = col.GetComponent<Rigidbody>();
				if (rb)
				{
					rb.AddExplosionForce(
						explosionForce: projectileInfo.blastForce,
						explosionPosition: transform.position,
						explosionRadius: projectileInfo.blastRadius,
						upwardsModifier: projectileInfo.blastForce / 2.0f,
						mode: ForceMode.Impulse);
				}
			}
		}

		
	}
}