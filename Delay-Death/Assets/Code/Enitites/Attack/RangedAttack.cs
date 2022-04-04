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
		[SerializeField] private float _shotSpeed;

		public override void Attack()
		{
			if (CurChillTime <= 0)
			{
				Animator.SetTrigger("Attack");
				Shoot();
			}
		}

		private void Shoot()
        {
			GameObject shotTransform = Instantiate(_shotPrefab, _firePoint.position, Quaternion.identity);
			shotTransform.GetComponent<ShotScript>().Trajectory = Direction;
			shotTransform.GetComponent<ShotScript>().Shooter = gameObject;
			//AudioManager.Instance.Play("Shot");

			CurChillTime = ChillTime; //Pause between attacks
			StartCoroutine(Cooldown());
		}

	}
}
