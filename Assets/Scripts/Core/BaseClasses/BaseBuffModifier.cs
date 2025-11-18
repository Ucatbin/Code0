using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [System.Serializable]
    public abstract class BaseBuffModifier : ScriptableObject
    {
        public abstract void Apply(BuffModel buffInfo);
    }
}