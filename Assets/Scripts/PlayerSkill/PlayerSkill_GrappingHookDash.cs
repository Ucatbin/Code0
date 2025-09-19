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
    [SerializeField] float _duration = 0.25f;

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
        TimerManager.Instance.AddTimer(
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
        float elapsedTime = 0f;
        while (dashSpeed != 1f && _player.IsAttached)
        {
            float t = elapsedTime / _duration;
            dashSpeed = Mathf.Lerp(dashSpeed, 1f, t);
            RopeJoint.distance -= dashSpeed * Time.fixedDeltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        CoolDownSkill(SkillCD, "PlayerSkill");
    }
}