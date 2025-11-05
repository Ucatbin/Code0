using UnityEngine;

namespace ThisGame.EntitySystem
{
    public class EntityPresenter
    {
        public void RegisterEntity<TModel>(EntityData data)
            where TModel : IEntityModel, new()
        {
            var model = new TModel();
        }
    }
}
