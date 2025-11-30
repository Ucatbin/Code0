using UnityEngine;
using System.Collections.Generic;
using ThisGame.Entity.SkillSystem;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.HealthSystem;
using ThisGame.Core;

public class P_DashAttackView : SkillView
{
    [Header("Dash Attack View")]
    [SerializeField] LineRenderer _line;
    [SerializeField] Transform player;
    [SerializeField] private Camera cam;
    
    [Header("Manual Sprite Objects")]
    [SerializeField] private List<GameObject> manualSpriteObjects = new List<GameObject>();
    
    [Header("Dash Settings")]
    [SerializeField] private float maxDashDistance = 10f;
    
    [Header("Debug Visualization")]
    [SerializeField] private bool showDebugLines = true;
    [SerializeField] private Color debugLineColor = Color.red;

    [SerializeField] PlayerController _player;
    [SerializeField] Collider2D _attackCollider;
    [SerializeField] LayerMask _canHit;
    List<Collider2D> _hitTargets = new List<Collider2D>();
    private bool isAiming;
    private LineRenderer debugLine;
    private List<Vector3> originalLocalPositions = new List<Vector3>();

    void OnEnable()
    {
        EventBus.Subscribe<AttackAnimationEvent>(this, HandleAttackAnim);
    }
    void HandleAttackAnim(AttackAnimationEvent @event)
    {
        switch (@event.AttackEventType)
        {
            case AttackEventType.ColliderEnable:
                _attackCollider.enabled = true;
                break;
            case AttackEventType.ColliderDisable:
                _attackCollider.enabled = false;
                break;
        }
    }
    void Awake()
    {
        _attackCollider.enabled = false;
        _attackCollider.callbackLayers = _canHit;
        
        CreateDebugLine();
        InitializeManualObjects();
        HideAim();
    }

    public void UpdateView()
    {
        if (isAiming)
        {
            UpdateAim();
            UpdateDebugVisualization();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (_hitTargets.Contains(other)) return;

        var target = other.transform.parent.GetComponent<EntityController>();
        if (target == null || target.GetController<HealthController>() == null) return;

        var damageInfo = new DamageInfo()
        {
            DamageSource = _player,
            DamageTarget = target,
            DamageAmount = 5
        };
        target.GetController<HealthController>().Model.TakeDamage(damageInfo);
    }
    public void StartAiming()
    {
        isAiming = true;
        ShowAim();
        ShowSpriteObjects();
    }

    public void StopAiming()
    {
        isAiming = false;
        HideAim();
        HideSpriteObjects();
    }

    // ---------------------------
    // 手动对象初始化
    // ---------------------------

    private void InitializeManualObjects()
    {
        // 保存每个对象的原始本地位置
        originalLocalPositions.Clear();
        
        foreach (var obj in manualSpriteObjects)
        {
            if (obj != null)
            {
                originalLocalPositions.Add(obj.transform.localPosition);
                obj.SetActive(false); // 默认隐藏
            }
        }
        
        Debug.Log($"Initialized {manualSpriteObjects.Count} manual sprite objects");
    }

    // ---------------------------
    // 瞄准线渲染
    // ---------------------------

    private void UpdateAim()
    {
        if (manualSpriteObjects.Count == 0) return;

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        DrawAimingCurve(mouseWorldPos);
        UpdateSpriteObjectsDistribution(mouseWorldPos);
    }

    private void DrawAimingCurve(Vector3 target)
    {
        if (_line != null)
        {
            _line.positionCount = 2;
            _line.SetPosition(0, player.position);
            _line.SetPosition(1, target);
        }
    }

    private void UpdateSpriteObjectsDistribution(Vector3 target)
    {
        Vector3 direction = (target - player.position).normalized;
        float totalDistance = Vector3.Distance(player.position, target);
        
        // 限制最大距离
        totalDistance = Mathf.Min(totalDistance, maxDashDistance);
        
        for (int i = 0; i < manualSpriteObjects.Count; i++)
        {
            if (manualSpriteObjects[i] == null) continue;
            
            float t = (float)i / (manualSpriteObjects.Count - 1);
            
            // 基础位置：在玩家到目标的连线上均匀分布
            Vector3 basePosition = player.position + direction * (totalDistance * t);
            
            // 叠加原始本地位置的偏移（转换到目标方向）
            Vector3 animationOffset = TransformAnimationOffset(originalLocalPositions[i], direction);
            
            Vector3 finalPosition = basePosition + animationOffset;
            manualSpriteObjects[i].transform.position = finalPosition;
            
            // 根据方向旋转对象
            // manualSpriteObjects[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    private Vector3 TransformAnimationOffset(Vector3 originalOffset, Vector3 targetDirection)
    {
        // 将原始本地位置偏移转换到目标方向
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, targetDirection);
        return rotation * originalOffset;
    }

    // ---------------------------
    // 调试可视化
    // ---------------------------

    private void CreateDebugLine()
    {
        GameObject debugLineObj = new GameObject("DebugLine");
        debugLineObj.transform.SetParent(transform);
        debugLine = debugLineObj.AddComponent<LineRenderer>();
        
        debugLine.positionCount = 2;
        debugLine.startWidth = 0.05f;
        debugLine.endWidth = 0.05f;
        debugLine.material = new Material(Shader.Find("Sprites/Default"));
        debugLine.startColor = debugLineColor;
        debugLine.endColor = debugLineColor;
        debugLine.enabled = false;
    }

    private void UpdateDebugVisualization()
    {
        if (!showDebugLines) return;

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        
        // 更新调试线
        debugLine.enabled = true;
        debugLine.SetPosition(0, player.position);
        debugLine.SetPosition(1, mouseWorldPos);
        
        // 计算并显示连线长度
        float distance = Vector3.Distance(player.position, mouseWorldPos);
        distance = Mathf.Min(distance, maxDashDistance);
        
        // 在Scene视图中显示距离信息
        Debug.DrawLine(player.position, mouseWorldPos, debugLineColor);
    }

    // ---------------------------
    // 工具方法
    // ---------------------------

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        
        // 假设在z=0的平面上
        Plane groundPlane = new Plane(Vector3.forward, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        
        return player.position + Vector3.right * 5f; // 备用位置
    }

    private void ShowSpriteObjects()
    {
        foreach (var obj in manualSpriteObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    private void HideSpriteObjects()
    {
        foreach (var obj in manualSpriteObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        if (debugLine != null)
            debugLine.enabled = false;
    }

    private void ShowAim()
    {
        if (_line != null)
            _line.enabled = true;
    }

    private void HideAim()
    {
        if (_line != null)
            _line.enabled = false;
    }
}