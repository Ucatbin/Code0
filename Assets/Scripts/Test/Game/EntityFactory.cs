
namespace ThisGame.EntitySystem
{
    public static class EntityFactory
    {
        public static IEntityModel CreateEntity<TModel, TData>(TData data)
            where TModel : IEntityModel, new()
            where TData : IEntityData
        {
            var model = new TModel();
            model.Initialize(data);
            return model;
        }
    }
}