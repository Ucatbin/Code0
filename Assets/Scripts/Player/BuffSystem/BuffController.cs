using System;
using System.Collections.Generic;
using System.Linq;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class BuffController : BaseController
    {
        protected Dictionary<Type, BuffModel> _models;
        [SerializeField] protected List<BuffModel> _activeBuffs;
        SortedSet<BuffModel> _buffHeap;

        public override void Initialize()
        {
            _models = new Dictionary<Type, BuffModel>();
            _activeBuffs = new List<BuffModel>();
            _buffHeap = new SortedSet<BuffModel>(_activeBuffs);

            RegisterModels();
        }
        public virtual void RegisterModels() { }

        public void AddBuff<T>(T thisBuff, int stacks) where T : BuffModel
        {
            Type newBuffType = thisBuff.GetType();
            BuffModel existingBuff = _activeBuffs.FirstOrDefault(buff => buff.GetType() == newBuffType);

            if (existingBuff != null && existingBuff.Data.BuffType != BuffType.Independent)
            {
                int stacksAdd = stacks;
                if (existingBuff.Data.BuffType == BuffType.Stackable)
                {
                    if (existingBuff.CurrentStacks < existingBuff.Data.MaxStacks)
                    {
                        if (existingBuff.CurrentStacks + stacks <= existingBuff.Data.MaxStacks)
                            stacksAdd = stacks;
                        else
                            stacksAdd = existingBuff.Data.MaxStacks - existingBuff.CurrentStacks + stacks;
                    }
                    else
                        stacksAdd = 0;
                }
                existingBuff.AddStack(stacksAdd);

                switch (existingBuff.Data.BuffStackType)
                {
                    case BuffStackType.Extend:
                        TimerManager.Instance.ExtendTimersWithTag(
                            existingBuff.Data.BuffName,
                            existingBuff.Data.Duration
                        );
                        break;
                    case BuffStackType.Refresh:
                        TimerManager.Instance.SetTimersWithTag(
                            existingBuff.Data.BuffName,
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
                    TimerManager.Instance.AddTimer(
                        thisBuff.Data.Duration,
                        () => RemoveBuff(thisBuff),
                        thisBuff.Data.BuffName
                    );
                if (thisBuff.Data.Interval != -1)
                    TimerManager.Instance.AddLoopTimer(
                        thisBuff.Data.Interval,
                        () => thisBuff.Data.OnInvoke?.Apply(thisBuff),
                        false,
                        thisBuff.Data.BuffName
                    );
                thisBuff.Data.OnCreat?.Apply(thisBuff);
                _activeBuffs.Add(thisBuff);
            }
        }
        void RemoveBuff<T>(T thisBuff) where T : BuffModel
        {
            switch (thisBuff.Data.BuffRemoveType)
            {
                case BuffRemoveType.Clear:
                    thisBuff.Data.OnRemove.Apply(thisBuff);
                    _activeBuffs.Remove(thisBuff);
                    TimerManager.Instance.CancelTimersWithTag(thisBuff.Data.BuffName);
                    break;
                case BuffRemoveType.Reduce:
                    thisBuff.ReduceStack(1);
                    thisBuff.Data.OnRemove.Apply(thisBuff);
                    if (thisBuff.CurrentStacks == 0)
                    {
                        _activeBuffs.Remove(thisBuff);
                        TimerManager.Instance.CancelTimersWithTag(thisBuff.Data.BuffName);
                    }
                    break;
            }
            _buffHeap.Remove(thisBuff);
        }

        public T GetBuff<T>() where T : BuffModel
        {
            if (_models.ContainsKey(typeof(T)))
                return _models[typeof(T)] as T;
            else
                return null;
        }
    }
}