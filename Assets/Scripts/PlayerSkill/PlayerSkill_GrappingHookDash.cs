using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }

    [Header("GHookAttribute")]
    [SerializeField] float _lineDashSpeed = 20f; // Maximum distance to detect grapple points
    [SerializeField] float _lineDashDamping = 8f;

    public PlayerSkill_GrappingHookDash(PlayerController player) : base(player) { }

    public override void BasicSkillCheck()
    {
        if (!_inputSys.DashTrigger || !CanUseSkill)
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        // TODO:Havent complete if yet
        if (!CanUseSkill)
            return;
        CanUseSkill = false;
        StartCoroutine(LineDash());
    }
    public override void CoolDownSkill()
    {
        Player_TimerManager.Instance.AddTimer(
            CoolDown,
            () => { ResetSkill(); },
            "Player_AbilityTimer"
        );
    }
    public override void ResetSkill()
    {
        CanUseSkill = true;
    }

    public IEnumerator LineDash()
    {
        float dashSpeed = _lineDashSpeed;
        while (dashSpeed != 1f && _player.IsAttached)
        {
            RopeJoint.distance -= _lineDashSpeed * Time.fixedDeltaTime;
            dashSpeed = Mathf.Lerp(dashSpeed, 1f, Time.fixedDeltaTime * _lineDashDamping);
            yield return null;
        }
        CoolDownSkill();
    }
}