using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ThisGame.Entity.SkillSystem
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance;
        Dictionary<Type, SkillEntry> _skillEntryMap;
        [SerializeField] protected SkillEntry[] _skillEnties;

        void Awake()
        {
            _skillEntryMap = new Dictionary<Type, SkillEntry>();

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            RegisterSkillMapping();
        }

        void RegisterSkillMapping()
        {
            _skillEntryMap.Clear();
            _skillEntryMap = _skillEnties.ToDictionary(entry => entry.SkillModelType, entry => entry);
        }
        public SkillEntry GetSkillEntry(Type skillType)
        {
            _skillEntryMap.TryGetValue(skillType, out SkillEntry entry);
            return entry;
        }
    }

    [Serializable]
    public class SkillEntry
    {
        [SerializeField] string _skillModelName;
        public SkillData Data;
        public SkillView View;
        Type _skillModelType;
        public Type SkillModelType
        {
            get
            {
                if (_skillModelType == null && !string.IsNullOrEmpty(_skillModelName))
                {
                    _skillModelType = Type.GetType($"ThisGame.Entity.SkillSystem.{_skillModelName}, Assembly-CSharp");
                    if (_skillModelType == null)
                        Debug.LogError($"Can't match: {_skillModelType}");

                }
                return _skillModelType;
            }
            set
            {
                _skillModelType = value;
                _skillModelName = value?.AssemblyQualifiedName;
            }
        }
    }
}