using UnityEngine;
using UnityEngine.Rendering;

public class AttackChecker : MonoBehaviour
{
    public PlayerSkill_Attack _attack;
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponentInChildren<EnemyController_Main>()?.TakeDamage(_attack.Damage);
    }
}