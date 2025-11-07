using System;
using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    public class MoveModel
    {
        // Events
        public event Action<Vector3> OnVelocityChanged;
        public event Action<bool> OnMovementStateChanged;

        Vector3 _inputDir;
        Vector3 _velocity;
        public Vector3 Velocity => _velocity;
        bool _isMoving;
        public bool IsMoving => _isMoving;

        // Dependency
        MoveData _data;
        public MoveData Data => _data;
        public MoveModel(MoveData data)
        {
            _data = data;
            _velocity = Vector3.zero;
        }

        public void UpdateMovement(Vector3 input, float deltaTime)
        {
            _inputDir = input.normalized;

            bool wasMoving = _isMoving;
            _isMoving = input.magnitude > 0.01f;

            if (wasMoving != _isMoving)
                OnMovementStateChanged?.Invoke(_isMoving);

            if (_isMoving)
            {
                Vector3 targetVelocity = _inputDir * _data.BaseSpeed;
                _velocity.x = Mathf.MoveTowards(
                    _velocity.x,
                    targetVelocity.x,
                    _data.Acceleration * deltaTime
                );
                Debug.Log(_velocity);
            }
            else
            {
                _velocity.x = Mathf.MoveTowards(
                    _velocity.x,
                    0f,
                    _data.Acceleration * deltaTime
                );
            }
            OnVelocityChanged?.Invoke(_velocity);
        }
        public void HandleGravity(float deltaTime)
        {
            _velocity.y = Mathf.MoveTowards(
                _velocity.y,
                -_data.MaxFallSpeed,
                _data.Gravity * deltaTime
            );
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            OnVelocityChanged?.Invoke(_velocity);
        }
    }
}