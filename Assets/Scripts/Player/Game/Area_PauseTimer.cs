using UnityEngine;

public class Area_PauseTimer : MonoBehaviour
{
    [SerializeField] Collider2D _area;
    [SerializeField] LayerMask _player;


    void Awake()
    {
        _area.callbackLayers = _player;
    }
    void OnTriggerEnter2D(Collider2D player)
    {
        TimerManager.Instance.PauseTimerWithTag("P_CountDown");
    }
    void OnTriggerExit2D(Collider2D player)
    {
        TimerManager.Instance.ResumeTimerWithTag("P_CountDown");
    }
}
