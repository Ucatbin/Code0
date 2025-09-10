using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : PlayerSkill_BaseSkill
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }

    [Header("GHookAttribute")]
    [SerializeField] float _lineDashSpeed = 20f; // Maximum distance to detect grapple points
    [SerializeField] float _lineDashDamping = 8f;

    public PlayerSkill_GrappingHookDash(PlayerController_Main player) : base(player) { }

    public override void TryUseSkill()
    {
        // TODO:Havent complete if yet
        if (!CanUseSkill ||
            CurrentCharges == 0 ||
            !_inputSys.DashTrigger
        )
            return;
        UseSkill();
    }
    public override void UseSkill()
    {
        CurrentCharges -= MaxCharges == -1 ? 0 : 1;
        CanUseSkill = false;

        StartCoroutine(LineDash());
    }
    public override void CoolDownSkill(float coolDown, string tag)
    {
        Player_TimerManager.Instance.AddTimer(
            coolDown,
            () => { ResetSkill(); },
            tag
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
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
}