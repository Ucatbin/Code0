using UnityEngine;

public class PlayerController_Visual : MonoBehaviour
{
    [SerializeField] PlayerController_Main _player;
    public TrailRenderer GlideTrail;
    public Transform PlayerVisual;

    void Update()
    {
        Flip();
    }

    void Flip()
    {
        if (_player.InputSys.MoveInput.x == 0f &&
                !_player.InputSys.AttackTrigger &&
                !_player.IsAttached
            )
            return;

        if (_player.Rb.linearVelocityX * _player.FacingDir < 0)
        {
            PlayerVisual.transform.Rotate(new Vector2(0f, 180f));
            _player.FacingDir = _player.FacingDir * -1;
        }
    }
}
