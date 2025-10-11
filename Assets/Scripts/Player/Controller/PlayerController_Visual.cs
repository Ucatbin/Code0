using UnityEngine;

public class PlayerController_Visual : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;
    public TrailRenderer GlideTrail;
    [SerializeField] Transform _playerVisual;
    [SerializeField] Transform _playerCheckers;

    void Update()
    {
        Flip();
    }

    public void Flip()
    {
        if (_player.InputSys.MoveInput.x == 0f &&   // Can flip while moving

                !_player.IsHooked ||              // Can flip while attached
                _player.IsWallSliding               // Precent flip shaking when is wall sliding
            )
            return;

        if (_player.Rb.linearVelocityX * _player.FacingDir < 0)
        {
            _player.Root.Rotate(new Vector2(0f, 180f));
            _player.FacingDir = _player.FacingDir * -1;
        }
    }
}