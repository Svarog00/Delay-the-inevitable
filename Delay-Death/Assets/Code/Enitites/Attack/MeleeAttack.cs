using Assets.Code.Enitites.Enemies;
using UnityEngine;

namespace Assets.Code.Enitites.Attack
{
    public class MeleeAttack : EntityAttack
    {
        public override void Attack()
        {
			if (CurChillTime <= 0)
			{
				AudioManager.Instance.Play("ZombieRoar");
				Animator.SetTrigger("Attack"); //animate
				Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackDistance, PlayerLayer); //find the player in circle                                                                                                      //damage him
				foreach (Collider2D enemy in hitEnemies)
				{
					if (enemy.tag.Contains("Player"))
					{
						enemy.GetComponent<IHealth>().Hurt(Damage);
					}
				}
				StartCoroutine(Cooldown());
			}
			
        }
    }
}
