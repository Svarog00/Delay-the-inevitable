using System.Collections;
using UnityEngine;

namespace Assets.Code.Enitites
{
    public abstract class EntityAttack : MonoBehaviour
    {
        public float AttackDistanceGetter => AttackDistance;

        public Vector2 Direction
        {
            get; set;
        }

        public float DistanceToPlayer
        {
            get; set;
        }
        
        [SerializeField] protected Transform AttackPoint = null;
        [SerializeField] protected Animator Animator;
        [SerializeField] protected int Damage;
        [SerializeField] protected float AttackDistance;
        [SerializeField] protected float ChillTime;

        protected float CurChillTime;

        public abstract void Attack();

        protected IEnumerator Cooldown()
        {
            CurChillTime = ChillTime;
            while (true)
            {
                CurChillTime -= Time.deltaTime;
                if (CurChillTime <= 0)
                {
                    yield break;
                }
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackDistance);
        }
    }
}
