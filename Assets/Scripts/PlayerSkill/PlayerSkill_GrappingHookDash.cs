using System.Collections;
using UnityEngine;

public class PlayerSkill_GrappingHookDash : Player_BaseSkill
{
    [Header("NecessaryComponent")]
    [field: SerializeField] public DistanceJoint2D RopeJoint { get; private set; }

    [Header("GHookAttribute")]
    [SerializeField] float _lineDashSpeed = 20f; // Maximum distance to detect grapple points
    [SerializeField] float _lineDashDamping = 8f;

    public PlayerSkill_GrappingHookDash(Player player) : base(player) { }

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

    public override void UseSkill()
    {
        if (!CanUseSkill)
            return;
        StartCoroutine(LineDash());
    }

    public IEnumerator LineDash()
    {
        CanUseSkill = false;
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