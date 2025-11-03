using System;
using ThisGame.AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbilityData", menuName = "Game/AbilitySystem/Player/New AttackAbilityData")]
public class AttackAbilityData : AbilityData
{
    [SerializeField] public DamageData Damage;
    [SerializeField] AnimationClip _animClip;
    public int AnimatorHash;
    public float AttackDuration = 0.28f;
    public float AttackForce = 3.5f;

    void OnValidate()
    {
        if (_animClip != null)
            AnimatorHash = _animClip.GetHashCode();
    }
}
