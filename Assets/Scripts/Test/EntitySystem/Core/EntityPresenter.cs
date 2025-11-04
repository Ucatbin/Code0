using UnityEngine;

namespace ThisGame.EntitySystem
{
    public class EntityPresenter
    {
        // Dependency
        public EntityPresenter()
        {

        }
        public void RegisterEntity<TModel>(EntityData data)
            where TModel : IEntityModel, new()
        {
            var model = new TModel();
            
        }
    }
}
