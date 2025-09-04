using UnityEngine;

public class Player_VisualController : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    int _facingDir = 1;

    void Update()
    {
        if (_player.InputSys.MoveInput.x == 0f && !_player.InputSys.AttackTrigger)
            return;

        if (_player.Rb.linearVelocityX * _facingDir < 0)
        {
            _player.Visual.transform.Rotate(new Vector2(0f, 180f));
            _facingDir = _facingDir * -1;
        }
    }
}
