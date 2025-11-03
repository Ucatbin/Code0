using ThisGame.AbilitySystem;
using UnityEngine;

namespace ThisGame.GameSystem
{
    public class GameBootstrap : MonoBehaviour
    {
        public AbilitySystemBootstrap AbilitySystem;
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
            ServiceLocator.Register(AbilitySystem);
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