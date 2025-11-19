using System.Linq;
using ThisGame.Core;
using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class EntityController : MonoBehaviour
    {
        public Rigidbody2D Rb;
        protected int _facingDir = 1;
        public int FacingDir => _facingDir;
        public BaseController[] Controllers;
        
        protected virtual void Awake()
        {
            
        }
        protected virtual void Start()
        {
            foreach (var controller in Controllers)
                controller.Initialize();
        }

        protected virtual void FixedUpdate()
        {
            
        }
        protected virtual void Update()
        {

        }

        public T GetController<T>() where T : BaseController
        {
            return Controllers.OfType<T>().FirstOrDefault();
        }
    }
}