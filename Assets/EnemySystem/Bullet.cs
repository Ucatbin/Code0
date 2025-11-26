using UnityEngine;
using ThisGame.Entity.EntitySystem;
using ThisGame.Entity.HealthSystem;
using ThisGame.Core;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = 10 * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            GameObject playerObj = collision.transform.gameObject;
            GameObject controller = playerObj.transform.Find("Controller").gameObject;
            PlayerController playerController = controller.GetComponent<PlayerController>();
            HealthController healthController = controller.GetComponent<HealthController>();
            DamageInfo damage = new DamageInfo();
            damage.DamageAmount = 2f;
            damage.DamageSource = null;
            damage.DamageTarget = playerController;
            healthController.Model.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
