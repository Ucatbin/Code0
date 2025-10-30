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
            ServiceLocator.Register<IEventBus>(new EventBus());
            
            Debug.Log("All system initialize sucess");
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
