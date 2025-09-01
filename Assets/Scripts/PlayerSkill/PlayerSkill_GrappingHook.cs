using UnityEngine;

public class PlayerSkill_GrappingHook : Player_BaseSkill
{
    [Header("NecessaryComponent")]
    public Camera MainCam;
    public LineRenderer LineRenderer;
    public DistanceJoint2D DistanceJoint;
    public Vector2 HookPoint; // The point where the hook is attached

    [Header("GHookAttribute")]
    public float MaxDetectDist = 20f; // Maximum distance to detect grapple points
    public LayerMask CanHookLayer; // Which layer can the hook attach to

    public PlayerSkill_GrappingHook(Player player) : base(player) { }

    void Update()
    {
        if (!_inputSys.GrapperTrigger)
            return;
        if (!_player.IsAttached && CanUseSkill)
            {
                UseSkill();
            }
    }
    public override void UseSkill()
    {
        // Get mouse position and calculate fire direction
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDir = (mousePos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            fireDir,
            MaxDetectDist,
            CanHookLayer
        );

        if (hit.collider != null)
        {
            HookPoint = hit.point;
            AttachHook();
        }
    }
    public override void ResetSkill()
    {
        throw new System.NotImplementedException();
    }

    void AttachHook()
    {
        // Let state machine know the player is attached
        GrappleEvent.TriggerHookAttached();
        _player.IsAttached = true;

        // Set connect point and enable distance joint
        DistanceJoint.distance = Vector2.Distance(transform.position, HookPoint);
        DistanceJoint.connectedAnchor = HookPoint;
        DistanceJoint.enabled = true;

        // Setup line renderer
        LineRenderer.SetPosition(0, HookPoint);
        LineRenderer.SetPosition(1, transform.position);
        LineRenderer.enabled = true;
    }
}