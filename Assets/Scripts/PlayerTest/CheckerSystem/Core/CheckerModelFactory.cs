using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public static class CheckerModelFactory
    {
        static readonly Dictionary<string, Type> _modelTypes;
        
        static CheckerModelFactory()
        {
            _modelTypes = new();

            var checkerModelType = typeof(CheckerModel);
            var types = Assembly.GetExecutingAssembly().GetTypes();
            
            foreach (var type in types)
            {
                if (type != checkerModelType && checkerModelType.IsAssignableFrom(type) && !type.IsAbstract)
                    _modelTypes[type.Name] = type;
            }
        }

        public static CheckerModel CreateModel(string typeName, CheckerData data, Transform checkPoint)
        {
            if (_modelTypes.TryGetValue(typeName, out var modelType))
            {
                try
                {
                    return Activator.CreateInstance(modelType, data, checkPoint) as CheckerModel;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to creat{typeName} Error: {e.Message}");
                }
            }
            else
                Debug.LogWarning($"MisMatch: {typeName}, using Default");
            
            return new CheckerModel(data, checkPoint);
        }
        
        public static string[] GetAvailableModelTypes()
        {
            return _modelTypes.Keys.ToArray();
        }
    }
}