using UnityEngine;

namespace ThisGame.EntitySystem
{
    public class EntitySystemBootstrap : MonoBehaviour
    {
        [SerializeField] EntityData[] _entityDataList;
        public EntityPresenter EntityPresenter;

        void Start()
        {
            EntityPresenter = new EntityPresenter();
        }

        void RegisterEntities()
        {

        }
    }
}