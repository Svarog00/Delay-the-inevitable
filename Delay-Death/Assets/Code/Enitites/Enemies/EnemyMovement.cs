using System;
using UnityEngine;

namespace Assets.Code.Enitites.Enemies
{ 
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        private readonly string SpeedAnimatorTag = "Speed";

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rb2;
        [SerializeField] private float _movementSpeed;

        private Vector2 _movement;
        private bool _faceRight = true;
        private float _currentSpeed;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb2 = GetComponent<Rigidbody2D>();
        }

        public void HandleMove(Vector2 vector)
        {
            HandleMove(vector.x, vector.y);
        }

        public void HandleMove(float x, float y)
        {
            _movement.x = x;
            _movement.y = y;

            _currentSpeed = _movementSpeed;

            AnimateMove();

            if (_movement != new Vector2(0, 0))
            {
                ChangeAnimationDir();
            }
        }

        public void Stop()
        {
            _movement = Vector2.zero;
            _currentSpeed = 0;
        }

        private void FixedUpdate()
        {
            _rb2.MovePosition(_rb2.position + _movement * _movementSpeed * Time.deltaTime);
        }

        private void ChangeAnimationDir()
        {
            Vector2 _direction = _movement;
            if (_direction.x > 0 && _faceRight == true)
            {
                Flip();
            }
            if (_direction.x < 0 && _faceRight == false)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _faceRight = !_faceRight;
            Vector3 Scaler = gameObject.transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        private void AnimateMove()
        {
            _animator.SetFloat(SpeedAnimatorTag, _currentSpeed);
        }
    }
}
