using System;
using ThisGame.Entity.BuffSystem;
using TMPro;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class P_BuffController : BuffController
    {
        [SerializeField] TextMeshProUGUI _display;
        void Start()
        {
            var buffEntry = GlobalBuffManager.Instance.GetBuffEntry(typeof(P_CountDownModel));
            var countdown = new P_CountDownModel(buffEntry.Data as P_CountDownData, null, null, _display);
            AddBuff(countdown, 1);
        }
    }
}