// using System;
// using System.Collections.Generic;
// using UnityEngine;

// public class BuffHandler : MonoBehaviour
// {
//     [SerializeField] List<BaseBuffModel> _buffList = new List<BaseBuffModel>();
//     public SortedSet<BaseBuffModel> BuffHeap => new SortedSet<BaseBuffModel>(_buffList);
//     public void AddBuff(BaseBuffModel thisBuff)
//     {
//         BaseBuffModel existingBuff = FindBuff(thisBuff.BuffData.Id);
//         // If entity already have this buff and it's not independent
//         if (existingBuff != null && existingBuff.BuffData.BuffType != BuffType.Independent)
//         {
//             existingBuff.CurrentStack +=
//                 (existingBuff.BuffData.BuffType == BuffType.Stackable) &&
//                 (existingBuff.CurrentStack < existingBuff.BuffData.MaxStacks)
//                     ? 1
//                     : 0;

//             switch (existingBuff.BuffData.BuffStackType)
//             {
//                 case BuffStackType.Extend:
//                     // Extend duration
//                     TimerManager.Instance.ExtendTimersWithTag(
//                         existingBuff.BuffData.Id,
//                         existingBuff.BuffData.Duration
//                     );
//                     break;
//                 case BuffStackType.Refresh:
//                     // Refresh duration
//                     TimerManager.Instance.SetTimersWithTag(
//                         existingBuff.BuffData.Id,
//                         existingBuff.BuffData.Duration
//                     );
//                     break;
//                 case BuffStackType.None:
//                     // Do nothing
//                     break;
//             }
//         }
//         else
//         {
//             if (thisBuff.BuffData.Duration != -1)
//                 // Duration
//                 TimerManager.Instance.AddTimer(
//                     thisBuff.BuffData.Duration,
//                     () => RemoveBuff(thisBuff),
//                     thisBuff.BuffData.Id
//                 );
//             // Tick callback
//             if (thisBuff.BuffData.Interval != -1)
//                 TimerManager.Instance.AddLoopTimer(
//                     thisBuff.BuffData.Interval,
//                     () => thisBuff.BuffData.OnInvoke?.Apply(thisBuff),
//                     false,
//                     thisBuff.BuffData.Id + "Loop"
//                 );
//             thisBuff.BuffData.OnCreat?.Apply(thisBuff);
//             _buffList.Add(thisBuff);
//         }
//         Debug.Log(thisBuff.BuffData.BuffName + BuffHeap.Count);
//     }
//     void RemoveBuff(BaseBuffModel thisBuff)
//     {
//         switch (thisBuff.BuffData.BuffRemoveType)
//         {
//             case BuffRemoveType.Clear:  // Clear this buff no matter how many stacks
//                 thisBuff.BuffData.OnRemove.Apply(thisBuff);
//                 _buffList.Remove(thisBuff);
//                 TimerManager.Instance.CancelTimersWithTag(thisBuff.BuffData.Id + "Loop");
//                 break;
//             case BuffRemoveType.Reduce: // Reduce 1 stack, remove if stack reduce to 0
//                 thisBuff.CurrentStack--;
//                 thisBuff.BuffData.OnRemove.Apply(thisBuff);
//                 if (thisBuff.CurrentStack == 0)
//                 {
//                     _buffList.Remove(thisBuff);
//                     TimerManager.Instance.CancelTimersWithTag(thisBuff.BuffData.Id + "Loop");
//                 }
//                 break;
//         }
//     }

//     BaseBuffModel FindBuff(int buffDataId)
//     {
//         foreach (var buffInfo in BuffHeap)
//         {
//             if (buffInfo.BuffData.Id == buffDataId)
//                 return buffInfo;
//         }
//         return default;
//     }
// }