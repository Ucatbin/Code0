using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "ThisGame/Entity/BuffSystem/BuffData")]
    public class BuffData : ScriptableObject
    {
        public string BuffName;
        public Sprite Icon;
        public int Priority;
        public int MaxStacks;
        public string[] Tags;
        [Header("Time Info")]
        public float Duration;
        public float Interval;
        [Header("Update")]
        public BuffType BuffType;
        public BuffStackType BuffStackType;
        public BuffRemoveType BuffRemoveType;
        [Header("Call Back")]
        /// <summary>
        /// Invoke when buff first added
        /// </summary>
        public BuffModifier OnCreat;
        /// <summary>
        /// Invoke when buff just removed
        /// </summary>
        public BuffModifier OnRemove;
        /// <summary>
        /// Invoke when buff should be handle based on timer
        /// </summary>
        public BuffModifier OnInvoke;

        /// <summary>
        /// Invoke when hit other
        /// </summary>
        public BuffModifier OnHit;
        /// <summary>
        /// Invoke when be hit
        /// </summary>
        public BuffModifier OnBeHit;
        /// <summary>
        /// Invoke when killed other
        /// </summary>
        public BuffModifier OnKill;
        /// <summary>
        /// Invoke when be killed
        /// </summary>
        public BuffModifier OnBekill;
    }
    public enum BuffType
    {
        Stackable,      // Can add stacks
        Independent,    // Isolated to invoke
    }
    public enum BuffStackType
    {
        Extend,
        Refresh,
        None
    }
    public enum BuffRemoveType
    {
        Reduce,
        Clear
    } 
}
