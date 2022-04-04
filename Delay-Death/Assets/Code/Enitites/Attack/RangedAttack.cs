using Assets.Code.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Enitites.Attack
{
    public class RangedAttack : EntityAttack
    {
		[SerializeField] private GameObject _shotPrefab;
		[SerializeField] private Transform _firePoint;

		public override void Attack()
		{
			if (CurChillTime <= 0)
			{
				Shoot();
			}
		}

		private void Shoot()
        {
			GameObject shotTransform = Instantiate(_shotPrefab, _firePoint.position, _firePoint.rotation.normalized);
			shotTransform.GetComponent<ShotScript>().Trajectory = new Vector2(5, 5) * Direction;
			shotTransform.GetComponent<ShotScript>().Shooter = gameObject;
			//AudioManager.Instance.Play("Shot");

			CurChillTime = ChillTime; //Pause between attacks
			StartCoroutine(Cooldown());
		}

	}
}
