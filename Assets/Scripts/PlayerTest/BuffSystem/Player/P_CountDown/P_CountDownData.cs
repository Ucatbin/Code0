using ThisGame.Entity.BuffSystem;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    [CreateAssetMenu(fileName = "CountDown BuffData", menuName = "ThisGame/Player/BuffSystem/CountDown/Data")]
    public class P_CountDownData : BuffData
    {
        [Header("Settings")]
        public float MaxCountDown = 10f;
    }
}