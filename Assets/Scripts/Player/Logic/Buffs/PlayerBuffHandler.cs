// using ThisGame.Entity.EntitySystem;
// using TMPro;
// using UnityEngine;

// public class PlayerBuffHandler : BuffHandler
// {
//     [SerializeField] PlayerController _player;
//     [SerializeField] TextMeshProUGUI _countDownDisplay;
//     void Start()
//     {
//         var Buff_CountDown = new BuffItem_CountDown(
//             BuffManager.Instance.BuffData_CountDown,
//             _player,
//             _player,
//             1,
//             _countDownDisplay);
//         AddBuff(Buff_CountDown);
//     }
// }