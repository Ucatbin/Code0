using System;
using UnityEngine;

namespace ThisGame.EntitySystem
{
    public interface IEntityView
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }

        event Action<Vector3> OnMoveExecute;
        event Action OnJumpExecute;

        void UpdateView(Vector3 position, Quaternion rotation);
        void HandleAnimation(string animationName, bool state);
    }
}