using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThisGame.Entity.SkillSystem;
using UnityEngine;

namespace ThisGame.Core.CheckerSystem
{
    public static class SkillModelFactory
    {
        static readonly Dictionary<string, Type> _modelTypes;

        static SkillModelFactory()
        {
            _modelTypes = new();

            var skillModelType = typeof(SkillModel);
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types)
            {
                if (type != skillModelType && skillModelType.IsAssignableFrom(type) && !type.IsAbstract)
                    _modelTypes[type.Name] = type;
            }
        }

public static SkillModel CreateModel(string typeName, SkillData data)
{
    if (_modelTypes.TryGetValue(typeName, out var modelType))
    {
        Debug.Log($"🔍 查找类型: {typeName} -> {modelType.FullName}");
        
        // 详细检查所有构造函数
        var allConstructors = modelType.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        Debug.Log($"🏗️ 所有构造函数 ({allConstructors.Length} 个):");
        
        foreach (var constructor in allConstructors)
        {
            var parameters = constructor.GetParameters();
            var paramInfo = string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
            var accessibility = constructor.IsPublic ? "public" : 
                               constructor.IsPrivate ? "private" : 
                               constructor.IsFamily ? "protected" : "internal";
            Debug.Log($"   {accessibility} {modelType.Name}({paramInfo})");
        }

        // 特别检查 (SkillData) 构造函数
        var skillDataConstructor = modelType.GetConstructor(
            BindingFlags.Public | BindingFlags.Instance, 
            null, 
            new[] { typeof(SkillData) }, 
            null);
            
        if (skillDataConstructor != null)
        {
            Debug.Log($"✅ 找到 (SkillData) 构造函数");
            try
            {
                var instance = skillDataConstructor.Invoke(new object[] { data }) as SkillModel;
                Debug.Log($"🎉 成功创建实例: {typeName}");
                return instance;
            }
            catch (Exception e)
            {
                Debug.LogError($"❌ 调用构造函数失败: {e.Message}");
                Debug.LogError($"📋 堆栈: {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogError($"❌ 未找到 (SkillData) 构造函数");
        }
    }
    
    return new SkillModel(data);
}
        
        public static string[] GetAvailableModelTypes()
        {
            return _modelTypes.Keys.ToArray();
        }
    }
}