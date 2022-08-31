using Assets.Code.Props;
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
			ShotScript shotScript = shotTransform.GetComponent<ShotScript>();
			shotScript.Trajectory = Direction;
			shotScript.Shooter = gameObject;
			shotScript.Damage = Damage;
			
			//AudioManager.Instance.Play("Shot");

			CurChillTime = ChillTime; //Pause between attacks
			StartCoroutine(Cooldown());
		}

	}
}
