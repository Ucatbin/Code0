using System;
using System.Reflection.Emit;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.MoveSystem
{
    public class MoveModel
    {
        // Events
        public event Action<Vector3> OnVelocityChanged;
        public event Action<bool> OnMovementStateChanged;

        int _facingDir;
        public int FacingDir => _facingDir;
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
            _facingDir = 1;
        }

        public void UpdateMovement(Vector3 input, float deltaTime)
        {
            _inputDir = input.normalized;
            HandleFlip(input);

            bool wasMoving = _isMoving;
            _isMoving = input.magnitude != 0;

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
        public void HandleFlip(Vector3 input)
        {
            if (_facingDir * input.x < 0f)
            {
                _facingDir *= -1;
                var flip = new FlipAction
                {
                    FacingDir = _facingDir
                };
                EventBus.Publish(flip);
            }
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            OnVelocityChanged?.Invoke(_velocity);
        }
    }
}