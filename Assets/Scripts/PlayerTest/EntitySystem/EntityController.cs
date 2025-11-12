using UnityEngine;

namespace ThisGame.Entity.EntitySystem
{
    public class EntityController : MonoBehaviour
    {
        public Rigidbody2D Rb;
        protected int _facingDir = 1;
        public int FacingDir => _facingDir;
        protected virtual void Awake()
        {
            
        }
        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {
            
        }
        protected virtual void Update()
        {

        }
    }
}