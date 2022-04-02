using Assets.Code.Enitites.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Code.Props
{
    public class ShotScript : MonoBehaviour //Enemy shot script
    {
        public Vector2 Trajectory
        {
            get => _trajectory;
            set => _trajectory = value;
        }
        public GameObject Shooter
        {
            get => _shooter;
            set => _shooter = value;
        }

        [SerializeField] private Vector2 _trajectory = new Vector2(0, 0);
        [SerializeField] private GameObject _shooter;
        [SerializeField] private int _damage = 0;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 3);
        }

        private void Update()
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 newPosition = currentPosition + _trajectory * Time.deltaTime;
            RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.GetComponent<IHealth>() != null || hit.collider.gameObject != _shooter)
                {
                    hit.collider.GetComponent<IHealth>().Hurt(_damage);
                    Destroy(gameObject);
                }
                if (hit.collider.tag == "Border")
                {
                    continue;
                }
                else
                {
                    Destroy(gameObject);
                    break;
                }
            }

            transform.position = newPosition;
        }
    }

}
