using System;
using System.Collections.Generic;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public abstract class BuffController : BaseController
    {
        [SerializeField] protected BuffModelEntry[] _buffEntries;
        protected Dictionary<Type, BuffModel> _models;
        [SerializeField] SortedSet<BuffModel> _buffHeap;

        public override void Initialize()
        {
            _models = new Dictionary<Type, BuffModel>();
            _buffHeap = new SortedSet<BuffModel>();

            RegisterModels();
        }
        public abstract void RegisterModels();

        public void AddBuff<T>(T thisBuff) where T : BuffModel
        {
            BuffModel existingBuff = GetBuff<T>();

            if (existingBuff != null && existingBuff.Data.BuffType != BuffType.Independent)
            {
                var addStack =
                    (existingBuff.Data.BuffType == BuffType.Stackable) &&
                    (existingBuff.CurrentStacks < existingBuff.Data.MaxStacks)
                        ? 1
                        : 0;
                existingBuff.AddStack(addStack);

                switch (existingBuff.Data.BuffStackType)
                {
                    case BuffStackType.Extend:
                        // Extend duration
                        TimerManager.Instance.ExtendTimersWithTag(
                            existingBuff.Data.Id,
                            existingBuff.Data.Duration
                        );
                        break;
                    case BuffStackType.Refresh:
                        // Refresh duration
                        TimerManager.Instance.SetTimersWithTag(
                            existingBuff.Data.Id,
                            existingBuff.Data.Duration
                        );
                        break;
                    case BuffStackType.None:
                        // Do nothing
                        break;
                }
            }
            else
            {
                if (thisBuff.Data.Duration != -1)
                    // Duration
                    TimerManager.Instance.AddTimer(
                        thisBuff.Data.Duration,
                        () => RemoveBuff(thisBuff),
                        thisBuff.Data.Id
                    );
                // Tick callback
                if (thisBuff.Data.Interval != -1)
                    TimerManager.Instance.AddLoopTimer(
                        thisBuff.Data.Interval,
                        () => thisBuff.Data.OnInvoke?.Apply(thisBuff),
                        false,
                        thisBuff.Data.Id + "Loop"
                    );
                thisBuff.Data.OnCreat?.Apply(thisBuff);
                _buffList.Add(thisBuff);
            }
            Debug.Log(thisBuff.Data.BuffName + BuffHeap.Count);
        }
        void RemoveBuff(BaseBuffModel thisBuff)
        {
            switch (thisBuff.BuffData.BuffRemoveType)
            {
                case BuffRemoveType.Clear:  // Clear this buff no matter how many stacks
                    thisBuff.BuffData.OnRemove.Apply(thisBuff);
                    _buffList.Remove(thisBuff);
                    TimerManager.Instance.CancelTimersWithTag(thisBuff.BuffData.Id + "Loop");
                    break;
                case BuffRemoveType.Reduce: // Reduce 1 stack, remove if stack reduce to 0
                    thisBuff.CurrentStack--;
                    thisBuff.BuffData.OnRemove.Apply(thisBuff);
                    if (thisBuff.CurrentStack == 0)
                    {
                        _buffList.Remove(thisBuff);
                        TimerManager.Instance.CancelTimersWithTag(thisBuff.BuffData.Id + "Loop");
                    }
                    break;
            }
        }
        public T GetBuff<T>() where T : BuffModel
        {
            if (_models.ContainsKey(typeof(T)))
                return _models[typeof(T)] as T;
            else
                return null;
        }
    }

    [Serializable]
    public class BuffModelEntry
    {
        public string BuffName;
        public BuffData Data;
    }
}