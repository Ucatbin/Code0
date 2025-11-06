using System;
using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    public class MoveModel
    {
        public event Action<Vector3> OnVelocityChanged;
        public event Action<bool> OnMovementStateChanged;

        Vector3 _inputDir;
        Vector3 _velocity;
        public Vector3 Velocity => _velocity;
        bool _isMoving;
        public bool IsMoving => _isMoving;

        // Dependency
        MoveData _data;
        public MoveData Data;
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
                _velocity = Vector3.MoveTowards(
                    _velocity,
                    targetVelocity,
                    _data.Acceleration * deltaTime
                );
            }
            else
            {
                _velocity = Vector3.MoveTowards(
                    _velocity,
                    Vector3.zero,
                    _data.Deceleration * deltaTime
                );
            }
            OnVelocityChanged?.Invoke(_velocity);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            OnVelocityChanged?.Invoke(_velocity);
        }
    }
}