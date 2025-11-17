using System;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class BuffModel : IComparable<BuffModel>
    {
        protected int _currentStacks;
        public int CurrentStacks => _currentStacks;
        protected EntityController _source;
        public EntityController Source => _source;
        protected EntityController _target;
        public EntityController Target => _target;

        // Dependency
        BuffData _data;
        public BuffData Data => _data;
        public BuffModel(BuffData data, int stacks, EntityController source, EntityController target)
        {
            _data = data;
            _currentStacks = stacks;
            _source = source;
            _target = target;
        }

        public void AddStack(int stacks)
        {
            _currentStacks += stacks;
        }
        public int CompareTo(BuffModel other)
        {
            if (other == null) return 1;
            return _data.Priority.CompareTo(other._data.Priority);
        }
    }
}
