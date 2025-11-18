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
            var data = GlobalBuffManager.Instance.GetDataForType<P_CountDownModel>() as P_CountDownData;
            var countdown = new P_CountDownModel(data, null, null, _display);
            AddBuff(countdown, 1);
        }
    }
}