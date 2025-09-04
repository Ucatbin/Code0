using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Rigidbody2D rd;
    public Animator animator;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        
    }
}
