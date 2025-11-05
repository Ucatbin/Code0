using System;

namespace ThisGame.AbilitySystem
{
    public static class AbilityFactory
    {
        public static IAbilityModel CreateAbility<TModel, TData>(TData data) 
            where TModel : IAbilityModel, new()
            where TData : IAbilityData
        {
            var model = new TModel();
            model.Initialize(data);
            return model;
        }
    }
}