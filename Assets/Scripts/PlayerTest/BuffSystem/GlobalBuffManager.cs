using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Entity.BuffSystem
{
    public class GlobalBuffManager : MonoBehaviour
    {
        public static GlobalBuffManager Instance;

        [SerializeField] protected BuffModelEntry[] _buffEntries;
        Dictionary<Type, BuffModel> _models;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        void RegisterModels()
        {
            foreach (var entry in _buffEntries)
            {
                if (!string.IsNullOrEmpty(entry.BuffName) && entry.Data != null)
                {
                    BuffModel model = entry.BuffName switch
                    {
                        // "P_DoubleJump" => new P_DoubleJumpModel(entry.Data),
                        _ => null
                    };

                    if (model != null)
                        _models[model.GetType()] = model;
                }
            }
        }
    }
}