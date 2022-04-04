using Assets.Code.Props;
using UnityEngine;

namespace Assets.Code.Enitites.Attack
{
    public class MixedAttack : EntityAttack
    {
        [SerializeField] private float _meleeRange;
        [SerializeField] private Vector2 _pushForce;
		
        [SerializeField] private GameObject _shotPrefab;
		[SerializeField] private Transform _firePoint;

		[SerializeField] private LayerMask _playerLayer;

        public override void Attack()
        {
			if (CurChillTime <= 0)
			{
				if (DistanceToPlayer > _meleeRange)
                {
                    Shoot();
                }
                else
                {
                    MeleeStrike();
                }

                CurChillTime = ChillTime; //Pause between attacks
				StartCoroutine(Cooldown());
			}
		}

        private void Shoot()
        {
            GameObject shotTransform = Instantiate(_shotPrefab, _firePoint.position, _firePoint.rotation.normalized);
            shotTransform.GetComponent<ShotScript>().Trajectory = new Vector2(5, 5) * -Direction;
            shotTransform.GetComponent<ShotScript>().Shooter = gameObject;
            AudioManager.Instance.Play("Shot");
        }

        private void MeleeStrike()
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(gameObject.transform.position, AttackDistance, _playerLayer); //find the player in circle
                                                                                                                               //damage them
            foreach (Collider2D target in hitTargets)
            {
                if (target.tag.Contains("Player"))
                {
                    target.GetComponent<IDamagable>().Hurt(Damage);
                    target.GetComponent<Rigidbody2D>().AddForce(-Direction * _pushForce);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackDistance);
            Gizmos.DrawWireSphere(AttackPoint.position, _meleeRange);
        }
    }
}
