using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class GlobalBuffManager : MonoBehaviour
    {
        public static GlobalBuffManager Instance;
        Dictionary<Type, BuffEntry> _buffEntryMap;
        [SerializeField] protected BuffEntry[] _buffEntries;

        void Awake()
        {
            _buffEntryMap = new Dictionary<Type, BuffEntry>();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            RegisterBuffMapping();
        }
        
        void RegisterBuffMapping()
        {
            _buffEntryMap.Clear();
            _buffEntryMap = _buffEntries.ToDictionary(entry => entry.BuffModelType, entry => entry);
        }
        public BuffEntry GetBuffEntry(Type buffType)
        {
            _buffEntryMap.TryGetValue(buffType, out BuffEntry entry);
            return entry;
        }
    }

    [Serializable]
    public class BuffEntry
    {
        [SerializeField] string _buffModelName;
        public BuffData Data;
        Type _buffModelType;
        public Type BuffModelType 
        {
            get
            {
                if (_buffModelType == null && !string.IsNullOrEmpty(_buffModelName))
                {
                    _buffModelType = Type.GetType($"ThisGame.Entity.BuffSystem.{_buffModelName}, Assembly-CSharp");
                    if (_buffModelType == null)
                        Debug.LogError($"Can't match: {_buffModelName}");
                }
                return _buffModelType;
            }
            set
            {
                _buffModelType = value;
                _buffModelName = value?.AssemblyQualifiedName;
            }
        }
    }
}