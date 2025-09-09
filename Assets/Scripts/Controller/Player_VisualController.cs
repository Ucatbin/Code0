using UnityEngine;

public class Player_VisualController : MonoBehaviour
{
    [SerializeField] PlayerController _player;

    void Update()
    {
        if (_player.InputSys.MoveInput.x == 0f && !_player.InputSys.AttackTrigger)
            return;

        if (_player.Rb.linearVelocityX * _player.FacingDir < 0)
        {
            _player.Visual.transform.Rotate(new Vector2(0f, 180f));
            _player.FacingDir = _player.FacingDir * -1;
        }
    }
}
