using UnityEngine;
using System.Collections;

namespace ThisGame.Entity.SkillSystem
{
    public class P_GrapplingHookView : SkillView
    {
        [Header("绳索渲染")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _launchDuration = 0.3f;
        [SerializeField] private AnimationCurve _launchCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float _waveAmplitude = 0.5f;
        [SerializeField] private int _launchSegments = 10;
        
        private Transform _startPoint;
        private Transform _endPoint;
        private bool _isRopeActive = false;
        private Coroutine _launchCoroutine;
        private bool _isInLaunchAnimation = false;
        
        private void Awake()
        {
            InitializeRopeRenderer();
        }
        
        private void Update()
        {
            if (_isRopeActive && _startPoint != null && _endPoint != null)
            {
                if (!_isInLaunchAnimation)
                    UpdateRopePosition();
            }
        }
        
        private void InitializeRopeRenderer()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();
                
            _lineRenderer.positionCount = 2;
            _lineRenderer.enabled = false;
            
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
        }
        
        public void EnableRope(Transform startPoint, Transform endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _isRopeActive = true;
            _lineRenderer.enabled = true;
            
            if (_launchCoroutine != null)
                StopCoroutine(_launchCoroutine);
                
            _launchCoroutine = StartCoroutine(PlayRopeLaunchAnimation());
        }
        
        public void DisableRope()
        {
            _isRopeActive = false;
            _lineRenderer.enabled = false;
            _isInLaunchAnimation = false;
            
            if (_launchCoroutine != null)
            {
                StopCoroutine(_launchCoroutine);
                _launchCoroutine = null;
            }
        }
        
        public void SetRopeTension(float tension)
        {
            float width = Mathf.Lerp(0.03f, 0.08f, tension);
            _lineRenderer.startWidth = width;
            _lineRenderer.endWidth = width;
        }
        
        private void UpdateRopePosition()
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = _startPoint.position;
            positions[1] = _endPoint.position;
            _lineRenderer.SetPositions(positions);
        }
        
        private IEnumerator PlayRopeLaunchAnimation()
        {
            _isInLaunchAnimation = true;
            float timer = 0f;
            
            // 发射动画期间使用更多分段来制作波浪效果
            _lineRenderer.positionCount = _launchSegments;
            
            while (timer < _launchDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / _launchDuration;
                float curvedProgress = _launchCurve.Evaluate(progress);
                
                // 每次都重新获取当前位置，确保起点位置实时更新
                Vector3 currentStartPos = _startPoint.position;
                Vector3 currentEndPos = _endPoint.position;
                Vector3 direction = (currentEndPos - currentStartPos).normalized;
                
                // 计算当前绳索长度
                float totalLength = Vector3.Distance(currentStartPos, currentEndPos);
                float currentLength = totalLength * curvedProgress;
                
                // 更新每个分段的位置
                Vector3[] positions = new Vector3[_launchSegments];
                for (int i = 0; i < _launchSegments; i++)
                {
                    float segmentProgress = i / (float)(_launchSegments - 1);
                    float waveProgress = segmentProgress * curvedProgress;
                    
                    // 基础位置 - 使用实时更新的起点位置
                    Vector3 basePos = currentStartPos + direction * currentLength * segmentProgress;
                    
                    // 添加波浪效果
                    if (progress < 0.9f)
                    {
                        float wave = Mathf.Sin(waveProgress * Mathf.PI * 4f + Time.time * 10f) * _waveAmplitude * (1f - progress);
                        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward);
                        basePos += perpendicular * wave;
                    }
                    
                    positions[i] = basePos;
                }
                
                _lineRenderer.SetPositions(positions);
                yield return null;
            }
            
            // 动画完成后切换回直线模式
            _lineRenderer.positionCount = 2;
            _isInLaunchAnimation = false;
            UpdateRopePosition(); // 确保最终位置正确
            _launchCoroutine = null;
        }
    }
}