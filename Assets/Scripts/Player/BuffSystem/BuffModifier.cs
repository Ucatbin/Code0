using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [System.Serializable]
    public abstract class BuffModifier : ScriptableObject
    {
        public abstract void Apply(BuffModel buff);
    }
}