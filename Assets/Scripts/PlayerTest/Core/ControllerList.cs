using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThisGame.Core
{
    [Serializable]
    public class ControllerEntry
    {
        public string ControllerName;
        public MonoBehaviour Controller;
    }

    [Serializable]
    public class ControllerList
    {
        public List<ControllerEntry> Controllers = new List<ControllerEntry>();
        
        public T GetController<T>(string name) where T : MonoBehaviour
        {
            var entry = Controllers.Find(c => c.ControllerName == name && c.Controller is T);
            return entry?.Controller as T;
        }
        
        public bool TryGetController<T>(string name, out T controller) where T : MonoBehaviour
        {
            controller = GetController<T>(name);
            return controller != null;
        }
    }
}