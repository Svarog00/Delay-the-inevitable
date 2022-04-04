using Assets.Code.Enitites.Enemies;
using UnityEngine;

namespace Assets.Code.Enitites.Attack
{
    public class MeleeAttack : EntityAttack
    {
		[SerializeField] private LayerMask _attackLayer;

		public override void Attack()
        {
			if (CurChillTime <= 0)
			{
				Strike();
			}
        }

        private void Strike()
        {
			//AudioManager.Instance.Play("ZombieRoar");
			Animator.SetTrigger("Attack"); //animate
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackDistance, _attackLayer); //find the player in circle                                                                                                      //damage him
			foreach (Collider2D enemy in hitEnemies)
			{
				if (!enemy.CompareTag(gameObject.tag))
				{
					enemy.GetComponent<IDamagable>().Hurt(Damage);
				}
			}
			StartCoroutine(Cooldown());
		}
    }
}
