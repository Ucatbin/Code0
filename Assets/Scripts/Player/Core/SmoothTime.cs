using UnityEngine;

public class SmoothTime : MonoBehaviour
{
    static float _timeScale = 1f;

    public static float SmoothDeltaTime { get; private set; }
    public static float DeltaTime => Time.unscaledDeltaTime * _timeScale;
    public static float FixedDeltaTime => Time.fixedUnscaledDeltaTime * _timeScale;

    private static float _simulatedTime;
    public static float SimulatedTime => _simulatedTime;

    const float SmoothFactor = 0.15f;

    public static void SetSmoothTimeScale(float scale)
    {
        _timeScale = Mathf.Clamp(scale, 0.01f, 1f);
    }

    void Update()
    {
        float targetDelta = Time.unscaledDeltaTime * _timeScale;
        SmoothDeltaTime = Mathf.Lerp(SmoothDeltaTime, targetDelta, SmoothFactor);

        _simulatedTime += Time.unscaledDeltaTime * _timeScale;
    }
}
