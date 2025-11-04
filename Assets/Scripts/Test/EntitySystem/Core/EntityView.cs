using System;
using UnityEngine;

namespace ThisGame.EntitySystem
{
    public class EntityView : MonoBehaviour, IEntityView
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public event Action<Vector3> OnMoveExecute;
        public event Action OnJumpExecute;

        public void HandleAnimation(string animationName, bool state)
        {
            
        }

        public void UpdateView(Vector3 position, Quaternion rotation)
        {
            
        }
    }
}