using UnityEngine;

public class PlayerController_Visual : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;
    public TrailRenderer GlideTrail;
    [SerializeField] Transform _playerVisual;
    [SerializeField] Transform _playerCheckers;

    void Update()
    {
        TryFlip();
    }

    public void TryFlip()
    {
        if (_player.InputSys.MoveInput.x == 0f &&   // Can flip while moving
                !_player.IsHooked ||              // Can flip while attached
                _player.CheckerSys.IsWallDected               // Prevent flip shaking when is wall sliding
            )
            return;

        if (_player.Rb.linearVelocityX * _player.FacingDir < 0)
            _player.HandleFlip();
    }
}