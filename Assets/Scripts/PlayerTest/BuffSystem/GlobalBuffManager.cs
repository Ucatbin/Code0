using System;
using System.Collections.Generic;
using System.Linq;
using ThisGame.Entity.EntitySystem;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class GlobalBuffManager : MonoBehaviour
    {
        public static GlobalBuffManager Instance;
        Dictionary<Type, BuffData> _typeToDataMap = new Dictionary<Type, BuffData>();
        [SerializeField] protected BuffModelEntry[] _buffEntries;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            RegisterBuffDataMapping();
        }
        
        void RegisterBuffDataMapping()
        {
            _typeToDataMap.Clear();
            
            _typeToDataMap[typeof(P_CountDownModel)] = GetDataByName("P_CountDown");
            // TODO: More mappings
            
            Debug.Log($"Register {_typeToDataMap.Count} buffs");
        }
        BuffData GetDataByName(string buffName)
        {
            var entry = _buffEntries.FirstOrDefault(e => e.BuffName == buffName);
            if (entry != null && entry.Data != null)
                return entry.Data;
            
            Debug.LogError($"Cant find '{buffName}' 's buffData");
            return null;
        }

        public BuffData GetDataForType<T>() where T : BuffModel
        {
            var buffType = typeof(T);
            if (_typeToDataMap.TryGetValue(buffType, out BuffData data))
                return data;
            
            Debug.LogError($"Buff: {buffType.Name} is not registered");
            return null;
        }
    }
}