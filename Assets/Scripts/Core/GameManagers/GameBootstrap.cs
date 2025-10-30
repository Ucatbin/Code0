using UnityEngine;

namespace GameSystem
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            // 确保只有一个GameBootstrap
            if (FindObjectsOfType<GameBootstrap>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            InitializeGlobalServices();
        }
        
        private void InitializeGlobalServices()
        {
            var eventBus = new EventBus();
            ServiceLocator.Register<IEventBus>(eventBus);
        }
        
        private void Start()
        {
            InitializeEnetitySystem();
        }
        
        private void InitializeEnetitySystem()
        {

        }
    }
}
