using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Enhanced
{
	[CreateAssetMenu(fileName = "ProjectileInfo", menuName = "Bombard/ProjectileInfo", order = 1)]
	public class ProjectileInfo : ScriptableObject
	{
		public float blastRadius = 1.0f;
		public float blastForce = 1.0f;
		public float blastAfterSec = 5.0f;
		public float speed = 10.0f;
		public float mass = 1.0f;
	}
}